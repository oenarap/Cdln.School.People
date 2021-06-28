using System;
using System.Linq;
using Windows.UI.Xaml;
using School.People.Core;
using Windows.UI.Xaml.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Cdln.School.People.Uwp.Messages;

namespace Cdln.School.People.Uwp.Lists
{
    public sealed partial class PeopleListProvider
    {
        private static readonly DependencyProperty PeopleProperty = DependencyProperty.Register(nameof(People), typeof(ICollectionView), typeof(PeopleListProvider), new PropertyMetadata(null, OnPeoplePropertyChanged));
        private static readonly DependencyProperty FilterKeyProperty = DependencyProperty.Register(nameof(FilterKey), typeof(string), typeof(PeopleListProvider), new PropertyMetadata(null, OnFilterKeyPropertyChanged));

        public ICollectionView People => (ICollectionView)GetValue(PeopleProperty);

        public string FilterKey
        {
            get { return (string)GetValue(FilterKeyProperty); }
            set { SetValue(FilterKeyProperty, value); }
        }


        private static void OnPeoplePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var model = (PeopleListProvider)d;
            if (e.OldValue is ICollectionView oldView) { oldView.CurrentChanged -= model.OnCurrentPersonChanged; }
            if (e.NewValue is ICollectionView view)
            {
                view.CurrentChanged += model.OnCurrentPersonChanged;
                var index = Math.Min(view.Count, model.Logger.GetLog(model.ContextKey));
                if (view.MoveCurrentToPosition(index)) { return; }
            }
            model.Hub.Dispatch(new NoCurrentPersonEvent(model.Id));
        }

        private void OnCurrentPersonChanged(object sender, object e)
        {
            ICollectionView view = (ICollectionView)sender;
            if (view.CurrentItem is IPerson currentPerson)
            {
                Logger.Log(ContextKey, view.CurrentPosition);
                Hub.Dispatch(new CurrentPersonChangedEvent(Id, currentPerson));
            }
            else { Hub.Dispatch(new NoCurrentPersonEvent(Id)); }
        }

        /// <remarks>
        /// 2021-06-27 - This app needs to implement this property change callback because
        /// NetStandard 2.0 does not support ICollectionView.Filter property.
        /// </remarks>
        private static void OnFilterKeyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var model = (PeopleListProvider)d;
            model.Cache.TryGetValue(model.ContextKey, out ObservableCollection<IPerson> cached);

            if (cached != null)
            {
                var key = (string)e.NewValue;

                if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
                {
                    var current = (IPerson)model.People?.CurrentItem;
                    model.SetPeoplePropetyValue(cached, current);
                }
                else
                {
                    var filtered = (from p in cached
                                    where p.LastName.Contains(key, StringComparison.OrdinalIgnoreCase) ||
                                    p.FirstName.Contains(key, StringComparison.OrdinalIgnoreCase) ||
                                    p.MiddleName.Contains(key, StringComparison.OrdinalIgnoreCase)
                                    select p).AsEnumerable();

                    model.SetPeoplePropetyValue(filtered);
                }
            }
        }

        private void SetPeoplePropetyValue(IEnumerable<IPerson> source, IPerson current = null)
        {
            var viewSource = new CollectionViewSource() { Source = source };
            SetValue(PeopleProperty, viewSource.View);
            if (People.Count > 0 && current != null) { People.MoveCurrentTo(current); }
        }

        private readonly Dictionary<string, ObservableCollection<IPerson>> Cache;
        private readonly IIndexLogger Logger;
        private string ContextKey;
    }
}
