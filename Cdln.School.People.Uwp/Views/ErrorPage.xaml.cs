using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Cdln.School.People.Uwp.Views
{
    public sealed partial class ErrorPage : Page
    {
        private static readonly DependencyProperty ErrorMessageProperty = DependencyProperty.Register(nameof(ErrorMessage), typeof(string), typeof(ErrorPage), new PropertyMetadata(null));

        public string ErrorMessage => (string)GetValue(ErrorMessageProperty);

        public ErrorPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            SetValue(ErrorMessageProperty, e.Parameter.ToString());
        }
    }
}
