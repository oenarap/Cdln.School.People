using System;
using Autofac;
using Windows.UI;
using Windows.UI.Xaml;
using System.Threading.Tasks;
using Apps.Communication.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.ViewManagement;
using Windows.ApplicationModel.Core;
using Cdln.School.People.Uwp.Messages;

namespace Cdln.School.People.Uwp
{
    public sealed partial class MainPage : Page, IHandle<PeopleContextChangedEvent>, IHandle<SericeStatusChangedEvent>
    {
        public static readonly DependencyProperty ContextsProperty = DependencyProperty.Register(nameof(Contexts), typeof(IPeopleContextsListViewModel), typeof(MainPage), new PropertyMetadata(null));
        private static readonly DependencyProperty UsernameProperty = DependencyProperty.Register(nameof(Username), typeof(string), typeof(MainPage), new PropertyMetadata(null));
        private static readonly DependencyProperty ServiceStatusTextProperty = DependencyProperty.Register(nameof(ServiceStatusText), typeof(string), typeof(MainPage), new PropertyMetadata(null));

        private readonly NavigationService NavigationService;
        private readonly NavigationContext NavigationContext;

        public IPeopleContextsListViewModel Contexts => (IPeopleContextsListViewModel)GetValue(ContextsProperty);
        public string Username => (string)GetValue(UsernameProperty);
        public string ServiceStatusText => (string)GetValue(ServiceStatusTextProperty);

        public MainPage()
        {
            this.InitializeComponent();

            this.Loaded += OnMainPageLoaded;
            this.appNavigationView.BackRequested += (sender, args) => OnBackRequested();

            NavigationService = new NavigationService(this.contentFrame);
            NavigationContext = new NavigationContext();
        }

        public async Task Handle(SericeStatusChangedEvent message)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                if (message.Data == ServiceStatus.Online) { VisualStateManager.GoToState(this, "Online", true); }
                else { VisualStateManager.GoToState(this, "Offline", true); }
            });
        }

        private void OnMainPageLoaded(object sender, RoutedEventArgs e)
        {
            var hub = App.Container.Resolve<IMessageHub>();
            hub.RegisterHandler<MainPage, PeopleContextChangedEvent>(this);
            hub.RegisterHandler<MainPage, SericeStatusChangedEvent>(this);
        }

        public Task Handle(PeopleContextChangedEvent message)
        {
            return new Task(() =>
            {
                if (message.Data?.AssociatedViewType is Type associatedType)
                {
                    NavigationService.Navigate(associatedType, NavigationContext);
                }
            });
        }

        private void OnBackRequested() => NavigationService.GoBack();
    }
}
