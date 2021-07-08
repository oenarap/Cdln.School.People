using System;
using Autofac;
using Windows.UI.Xaml;
using School.People.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Cdln.School.People.Uwp.Lists;
using Cdln.School.People.Uwp.ViewModels;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace Cdln.School.People.Uwp.Views.Attributes
{
    public sealed partial class PersonalInformationView : Page
    {
        private static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel), typeof(object), typeof(PersonalInformationView), new PropertyMetadata(null));

        public object ViewModel => (object)GetValue(ViewModelProperty);

        public PersonalInformationView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SetupPage(e.Parameter).FireAndForget(false);
            base.OnNavigatedTo(e);
        }

        private async Task SetupPage(object param)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if (param is IPerson person)
                {
                    // TODO: request for data
                }
                else
                {
                    // TODO: reset content & lock the page for editing
                }
            });
        }
    }
}
