using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using School.People.Core;
using System.Threading.Tasks;
using Apps.Communication.Core;
using Cdln.School.People.Uwp.Messages;

namespace Cdln.School.People.Uwp.ViewModels
{
    public class AuxiliaryViewModel : MessagingModel, IContentHost, IHandle<CurrentPersonChangedEvent>
    {
        private static readonly DependencyProperty ContentProperty = DependencyProperty.Register(nameof(Content), typeof(object), typeof(AuxiliaryViewModel), new PropertyMetadata(null));

        public object Content => GetValue(ContentProperty);

        public async Task Handle(CurrentPersonChangedEvent message)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                try
                {
                    if (message.Data is IPerson person)
                    {
                        // TODO:
                    }
                }
                catch
                {
                    // TODO:
                }
            });
        }

        protected override void RegisterHandledMessages(IMessageHub hub)
        {
            base.RegisterHandledMessages(hub);
            Hub.RegisterHandler<AuxiliaryViewModel, CurrentPersonChangedEvent>(this);
        }

        protected override void UnregisterHandledMessages(IMessageHub hub)
        {
            base.UnregisterHandledMessages(hub);
            Hub.UnregisterHandler<AuxiliaryViewModel, CurrentPersonChangedEvent>(this);
        }

        public AuxiliaryViewModel(IMessageHub hub) : base(hub) { }
    }
}
