using System;
using Windows.System;
using Windows.UI.Xaml;
using School.People.Core;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using System.Collections.Generic;
using Cdln.School.People.Uwp.Views;
using Cdln.School.People.Uwp.Models;
using Cdln.School.People.Uwp.Messages;

namespace Cdln.School.People.Uwp.Lists
{
    public class PeopleContextsProvider : MessagingModel, IContextProvider
    {
        private static readonly DependencyProperty ContextsProperty = DependencyProperty.Register(nameof(Contexts), typeof(ICollectionView), typeof(PeopleContextsProvider), new PropertyMetadata(null, OnContextsPropertyChanged));
        private static readonly DependencyProperty CurrentProperty = DependencyProperty.Register(nameof(Current), typeof(PeopleContextDescriptor), typeof(PeopleContextsProvider), new PropertyMetadata(null));

        public ICollectionView Contexts => (ICollectionView)GetValue(ContextsProperty);
        public PeopleContextDescriptor Current => (PeopleContextDescriptor)GetValue(CurrentProperty);

        private static void OnContextsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var model = (PeopleContextsProvider)d;
            if (e.NewValue is ICollectionView view)
            {
                view.CurrentChanged += model.OnCurrentContextChanged;
                var index = Math.Min(view.Count, model.Logger.GetLog(model.Logkey));
                if (view.MoveCurrentToPosition(index)) { return; }
            }
            model.Hub.Dispatch(new NoCurrentPeopleContextEvent(model.Id));
        }

        private void OnCurrentContextChanged(object sender, object e)
        {
            var view = (ICollectionView)sender;
            if (view.CurrentItem is PeopleContextDescriptor newValue)
            {
                var oldValue = Current;
                SetValue(CurrentProperty, newValue);

                Logger.Log(Logkey, view.CurrentPosition);
                Hub.Dispatch(new PeopleContextChangedEvent(Id, (newValue, oldValue)));
            }
            else { Hub.Dispatch(new NoCurrentPeopleContextEvent(Id)); }
        }

        public PeopleContextsProvider(IMessageHub hub, IIndexLogger logger)
            : base(hub) {

            Logger = logger;
            Logkey = $"KEY_{nameof(PeopleContextsProvider)}";
            Logger.RegisterKey(Logkey);

            var viewSource = new CollectionViewSource() { Source = contexts };
            SetValue(ContextsProperty, viewSource.View);
        }

        private readonly string Logkey;
        private readonly IIndexLogger Logger;
        private static readonly List<PeopleContextDescriptor> contexts = new List<PeopleContextDescriptor>()
        {
            new PeopleContextDescriptor(PeopleContext.Personnel, "Personnel", typeof(ActivePeopleView),
                "", new KeyboardAccelerator() { Key = VirtualKey.F12 }),
            new PeopleContextDescriptor(PeopleContext.Students, "Students", typeof(ActivePeopleView),
                "", new KeyboardAccelerator() { Key = VirtualKey.F10 }),
            new PeopleContextDescriptor(PeopleContext.Others, "Others", typeof(InactivePeopleView),
                "", new KeyboardAccelerator() { Key = VirtualKey.F11 }),
            new PeopleContextDescriptor(PeopleContext.Archived, "Archived", typeof(InactivePeopleView),
                "", new KeyboardAccelerator() { Key = VirtualKey.F12 })
        };
    }
}
