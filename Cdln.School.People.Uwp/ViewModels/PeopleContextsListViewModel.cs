using System;
using Windows.UI.Xaml;
using School.People.Core;
using Windows.UI.Xaml.Data;
using Cdln.School.People.Uwp.Lists;
using Cdln.School.People.Uwp.Models;
using Cdln.School.People.Uwp.Messages;

namespace Cdln.School.People.Uwp.ViewModels
{
    public class PeopleContextsListViewModel : DependencyObject, IPeopleContextsListViewModel
    {
        private static readonly DependencyProperty ViewProperty = DependencyProperty.Register(nameof(View), typeof(ICollectionView), typeof(PeopleContextsListViewModel), new PropertyMetadata(null, OnViewPropertyChanged));

        private static void OnViewPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PeopleContextsListViewModel model = (PeopleContextsListViewModel)d;
            if (e.NewValue is ICollectionView view)
            {
                view.CurrentChanged += model.OnViewCurrentChanged;
                int index = Math.Min(view.Count, model.Logger.GetLog(model.Logkey));
                if (view.MoveCurrentToPosition(index)) { return; }
            }
            model.MessageHub.Dispatch(new NoCurrentPeopleContextEvent(model.Id));
        }

        private void OnViewCurrentChanged(object sender, object e)
        {
            ICollectionView view = (ICollectionView)sender;
            if (view.CurrentItem is PeopleContextDescriptor descriptor)
            {
                Logger.Log(Logkey, view.CurrentPosition);
                MessageHub.Dispatch(new PeopleContextChangedEvent(Id, descriptor));
            }
            else { MessageHub.Dispatch(new NoCurrentPeopleContextEvent(Id)); }
        }

        public PeopleContextsListViewModel(IMessageHub messageHub, IIndexLogger logger)
        {
            MessageHub = messageHub ?? throw new ArgumentNullException(nameof(messageHub));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));

            Logger.RegisterKey(Logkey);

            CollectionViewSource viewSource = new CollectionViewSource() { Source = PeopleContextDescriptors.Items };
            SetValue(ViewProperty, viewSource.View);
        }

        public ICollectionView View => (ICollectionView)GetValue(ViewProperty);

        private readonly string Logkey = "people_context";
        private readonly Guid Id = Guid.NewGuid();
        private readonly IIndexLogger Logger;
        private readonly IMessageHub MessageHub;
    }
}
