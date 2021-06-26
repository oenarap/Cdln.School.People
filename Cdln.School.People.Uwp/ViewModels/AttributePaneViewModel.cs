using System;
using Autofac;
using Windows.UI.Xaml;
using System.Threading.Tasks;
using Apps.Communication.Core;
using Windows.UI.Xaml.Controls;
using Cdln.School.People.Uwp.Models;
using Cdln.School.People.Uwp.Messages;

namespace Cdln.School.People.Uwp.ViewModels
{
    public class AttributePaneViewModel : DependencyObject, IHandle<AttributeContextChangedEvent>
    {
        private static readonly DependencyProperty PageProperty = DependencyProperty.Register(nameof(Page), typeof(Page), typeof(AttributePaneViewModel), new PropertyMetadata(null));

        public async Task Handle(AttributeContextChangedEvent message)
        {
            if (message.Data.NewValue is AttributeContextDescriptor context) { await Set(context); }
        }

        private async Task Set(AttributeContextDescriptor context)
        {
            Page page = null;
            if (context.AssociatedViewType is Type attribType)
            {
                if (App.Container.Resolve(attribType) is Page p) { page = p; }
            }
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => SetValue(PageProperty, page));
        }

        public AttributePaneViewModel(IMessageHub messageHub, AttributeContextsListViewModel attributeContextsListViewModel)
        {
            MessageHub = messageHub ?? throw new ArgumentNullException(nameof(messageHub));
            if (attributeContextsListViewModel?.View?.CurrentItem is AttributeContextDescriptor context) { Set(context).FireAndForget(false); }
            MessageHub.RegisterHandler<AttributePaneViewModel, AttributeContextChangedEvent>(this);
        }

        public Page Page => (Page)GetValue(PageProperty);
        private readonly IMessageHub MessageHub;
    }
}
