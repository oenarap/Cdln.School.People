using System;
using Autofac;
using Windows.UI.Xaml;
using Windows.UI.Core;
using System.Threading.Tasks;
using Apps.Communication.Core;
using Windows.UI.Xaml.Controls;
using Cdln.School.People.Uwp.Lists;
using Cdln.School.People.Uwp.Views;
using Cdln.School.People.Uwp.Messages;

namespace Cdln.School.People.Uwp
{
    public sealed partial class MainPage : Page, IHandle<PeopleContextChangedEvent>, IHandle<SericeStatusChangedEvent>
    {
        public static readonly DependencyProperty ContextsProviderProperty = DependencyProperty.Register(nameof(ContextsProvider), typeof(IContextProvider), typeof(MainPage), new PropertyMetadata(null));
        private static readonly DependencyProperty UsernameProperty = DependencyProperty.Register(nameof(Username), typeof(string), typeof(MainPage), new PropertyMetadata(null));
        private static readonly DependencyProperty ServiceStatusTextProperty = DependencyProperty.Register(nameof(ServiceStatusText), typeof(string), typeof(MainPage), new PropertyMetadata(null));

        private readonly NavigationService NavigationService;
        private readonly NavigationContext NavigationContext;

        public IContextProvider ContextsProvider => (IContextProvider)GetValue(ContextsProviderProperty);
        public string Username => (string)GetValue(UsernameProperty);
        public string ServiceStatusText => (string)GetValue(ServiceStatusTextProperty);

        public MainPage()
        {
            this.InitializeComponent();

            this.appNavigationView.BackRequested += (sender, args) => NavigationService.GoBack();

            NavigationService = new NavigationService(this.contentFrame);
            NavigationContext = new NavigationContext(NavigationService);

            var hub = App.Container.Resolve<IMessageHub>();
            
            hub.RegisterHandler<MainPage, PeopleContextChangedEvent>(this);
            hub.RegisterHandler<MainPage, SericeStatusChangedEvent>(this);

            NavigationContext.Add(hub);
            SetValue(ContextsProviderProperty, App.Container.Resolve<PeopleContextsProvider>());
        }

        public async Task Handle(SericeStatusChangedEvent message)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if (message.Data == ServiceStatus.Online) { VisualStateManager.GoToState(this, "Online", true); }
                else { VisualStateManager.GoToState(this, "Offline", true); }
            });
        }

        public async Task Handle(PeopleContextChangedEvent message)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                try
                {
                    var context = message.Data.NewValue;

                    if (context?.AssociatedViewType is Type type)
                    {
                        NavigationContext.Add(context);
                        NavigationService.Navigate(type, NavigationContext);
                    }
                    else
                    {
                        NavigationService.Navigate(typeof(Blank));
                    }
                }
                catch (Exception ex)
                {
                    NavigationContext.Add(ex);
                    NavigationService.Navigate(typeof(ErrorPage), NavigationContext);
                }
            });
        }

        private void OnAppNavigationViewItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked == true)
            {
                NavigationService.Navigate(typeof(SettingsView));
                return;
            }

            var contextFromItem = args.InvokedItem as IContextDescriptor;
            var contextFromContainer = args.InvokedItemContainer?.DataContext as IContextDescriptor;
            var context = contextFromItem ?? contextFromContainer;

            if (context != null)
            {
                ContextsProvider.Contexts.MoveCurrentTo(context);
                return;
            }

            NavigationService.Navigate(typeof(Blank));
        }
    }
}
