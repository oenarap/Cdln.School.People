using Autofac;
using Windows.UI.Xaml;
using Cdln.School.Content.Uwp;
using Windows.UI.Xaml.Controls;

namespace Cdln.School.People.Uwp.Views.Panes
{
    public sealed partial class Auxiliary : Page
    {
        private static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel), typeof(AuxiliaryViewModel), typeof(Auxiliary), new PropertyMetadata(null));

        public AuxiliaryViewModel ViewModel => (AuxiliaryViewModel)GetValue(ViewModelProperty);

        public Auxiliary()
        {
            this.InitializeComponent();

            var model = App.Container.Resolve<AuxiliaryViewModel>();
            SetValue(ViewModelProperty, model);
        }
    }
}