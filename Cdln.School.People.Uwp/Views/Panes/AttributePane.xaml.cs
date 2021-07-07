using System;
using Autofac;
using Windows.UI.Xaml;
using Windows.UI.Core;
using System.Threading.Tasks;
using Apps.Communication.Core;
using Windows.UI.Xaml.Controls;
using Cdln.School.People.Uwp.Lists;
using Cdln.School.People.Uwp.Messages;
using Windows.UI.Xaml.Navigation;
using School.People.Core;

namespace Cdln.School.People.Uwp.Views.Panes
{
    public sealed partial class AttributePane : Page, IHandle<AttributeContextChangedEvent>
    {
        private static readonly DependencyProperty ContextProviderProperty = DependencyProperty.Register(nameof(ContextProvider), typeof(AttributeContextsProvider), typeof(AttributePane), new PropertyMetadata(null));

        public AttributeContextsProvider ContextProvider => (AttributeContextsProvider)GetValue(ContextProviderProperty);


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                if (e.Parameter is NavigationContext navContext)
                {
                    var attributeContexts = navContext.GetInstance<AttributeContextsProvider>();
                    var currentContext = attributeContexts.Current;
                    var peopleProvider = navContext.GetInstance<PeopleListProvider>();
                    var person = peopleProvider?.People?.CurrentItem as IPerson;
                    var type = currentContext?.AssociatedViewType;
                    var hub = navContext.GetInstance<IMessageHub>();

                    hub?.RegisterHandler<AttributePane, AttributeContextChangedEvent>(this);
                    SetValue(ContextProviderProperty, attributeContexts);

                    if (type != null)
                    {
                        contentFrame.Navigate(type, person);
                        return;
                    }
                }

                contentFrame.Navigate(typeof(Blank));
            }
            catch (Exception ex)
            {
                contentFrame.Navigate(typeof(ErrorPage), ex);
            }
        }


        public async Task Handle(AttributeContextChangedEvent message)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                try
                {
                    if (message.Data.NewValue?.AssociatedViewType is Type type)
                    {
                        contentFrame.Navigate(type);
                    }
                    else
                    {
                        contentFrame.Navigate(typeof(Blank));
                    }
                }
                catch
                {
                    contentFrame.Navigate(typeof(ErrorPage));
                }
            });
        }

        public Command MoveNext
        {
            get
            {
                if (moveNext != null) { return moveNext; }
                if (moveNext == null)
                {
                    moveNext = new Command((param) => 
                    {
                        var index = ContextProvider.Contexts.CurrentPosition + 1;
                        ContextProvider.Contexts.MoveCurrentToPosition(index);
                    }, (param) =>
                    {
                        var index = ContextProvider.Contexts.CurrentPosition;
                        return (index + 1) < ContextProvider.Contexts.Count;
                    }, ContextProvider.Contexts, null);
                }
                return moveNext;
            }
        }

        public Command MovePrevious
        {
            get
            {
                if (movePrevious != null) { return movePrevious; }
                if (movePrevious == null)
                {
                    movePrevious = new Command((param) =>
                    {
                        var index = ContextProvider.Contexts.CurrentPosition - 1;
                        ContextProvider.Contexts.MoveCurrentToPosition(index);
                    }, (param) =>
                    {
                        var index = ContextProvider.Contexts.CurrentPosition;
                        return (index - 1) >= 0;
                    }, ContextProvider.Contexts, null);
                }
                return movePrevious;
            }
        }

        public AttributePane() => this.InitializeComponent();

        private Command moveNext;
        private Command movePrevious;
    }
}
