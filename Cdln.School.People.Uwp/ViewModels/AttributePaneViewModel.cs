using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using System.Threading.Tasks;
using Apps.Communication.Core;
using Cdln.School.People.Uwp.Views;
using Cdln.School.People.Uwp.Messages;

namespace Cdln.School.People.Uwp.ViewModels
{
    public class AttributePaneViewModel : MessagingModel, IContentHost, IHandle<AttributeContextChangedEvent>
    {
        private static readonly DependencyProperty ContentProperty = DependencyProperty.Register(nameof(Content), typeof(object), typeof(AttributePaneViewModel), new PropertyMetadata(null));

        public object Content => GetValue(ContentProperty);

        public async Task Handle(AttributeContextChangedEvent message)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                //try
                //{
                //    if (message.Data.NewValue?.AssociatedViewType is Type type)
                //    {
                //        SetValue(ContentProperty, Activator.CreateInstance(type));
                //    }
                //}
                //catch
                //{
                //    SetValue(ContentProperty, Activator.CreateInstance(typeof(ErrorPage)));
                //}
            });
        }

        protected override void RegisterHandledMessages(IMessageHub hub)
        {
            base.RegisterHandledMessages(hub);
            Hub.RegisterHandler<AttributePaneViewModel, AttributeContextChangedEvent>(this);
        }

        protected override void UnregisterHandledMessages(IMessageHub hub)
        {
            base.UnregisterHandledMessages(hub);
            Hub.UnregisterHandler<AttributePaneViewModel, AttributeContextChangedEvent>(this);
        }

        public AttributePaneViewModel(IMessageHub hub) : base(hub) { }
    }
}
