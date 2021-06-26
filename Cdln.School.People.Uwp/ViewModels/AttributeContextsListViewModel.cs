using System;
using Windows.UI.Xaml;
using School.People.Core;
using Windows.UI.Xaml.Data;
using System.Threading.Tasks;
using Apps.Communication.Core;
using System.Collections.Generic;
using Cdln.School.People.Uwp.Lists;
using Cdln.School.People.Uwp.Models;
using Cdln.School.People.Uwp.Messages;

namespace Cdln.School.People.Uwp.ViewModels
{
    public class AttributeContextsListViewModel : DependencyObject, IHandle<PeopleContextChangedEvent>
    {
        private static readonly DependencyProperty ViewProperty = DependencyProperty.Register(nameof(View), typeof(ICollectionView), typeof(AttributeContextsListViewModel), new PropertyMetadata(null, OnViewPropertyChanged));

        private static void OnViewPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AttributeContextsListViewModel model = (AttributeContextsListViewModel)d;
            if (e.NewValue is ICollectionView view)
            {
                view.CurrentChanged += model.OnViewCurrentChanged;
                int index = Math.Min(view.Count, model.Logger.GetLog(model.Logkey));
                view.MoveCurrentToPosition(index);
            }
        }

        private void OnViewCurrentChanged(object sender, object e)
        {
            ICollectionView view = (ICollectionView)sender;
            if (view.CurrentItem is AttributeContextDescriptor newValue)
            {
                Logger.Log(Logkey, view.CurrentPosition);
                AttributeContextDescriptor oldValue = Current;
                Current = newValue;
                MessageHub.Dispatch(new AttributeContextChangedEvent(Id, (newValue, oldValue)));
            }
        }

        public async Task Handle(PeopleContextChangedEvent message)
        {
            if (message.Data is PeopleContextDescriptor context) { await Set(context); }
        }

        private async Task Set(PeopleContextDescriptor context)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                Logkey = $"{context.Description}Attributes";
                Logger.RegisterKey(Logkey);
                switch (context.Description)
                {
                    case PeopleContext.Students:
                        Attributes = StudentsAttributesContextDescriptors.Items;
                        break;
                    case PeopleContext.Personnel:
                        Attributes = PersonnelAttributesContextDescriptors.Items;
                        break;
                    default:
                        Attributes = null;
                        break;
                }
                CollectionViewSource source = new CollectionViewSource() { Source = Attributes };
                SetValue(ViewProperty, source.View);
            });
        }

        public ICollectionView View => (ICollectionView)GetValue(ViewProperty);

        public AttributeContextsListViewModel(IMessageHub messageHub, IIndexLogger logger, IPeopleContextsListViewModel peopleContextsListViewModel)
        {
            MessageHub = messageHub ?? throw new ArgumentNullException(nameof(messageHub));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            MessageHub.RegisterHandler<AttributeContextsListViewModel, PeopleContextChangedEvent>(this);
            if (peopleContextsListViewModel.View.CurrentItem is PeopleContextDescriptor context) { Set(context).FireAndForget(false); }
        }

        private readonly Guid Id = Guid.NewGuid();
        private IEnumerable<AttributeContextDescriptor> Attributes;
        private readonly IIndexLogger Logger;
        private readonly IMessageHub MessageHub;
        private AttributeContextDescriptor Current;
        private string Logkey;
    }
}
