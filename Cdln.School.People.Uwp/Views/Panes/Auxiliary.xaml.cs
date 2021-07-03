using Autofac;
using Windows.UI.Xaml;
using Cdln.School.Content.Uwp;
using Windows.UI.Xaml.Controls;
using Cdln.School.People.Uwp.ViewModels.Auxiliaries;

namespace Cdln.School.People.Uwp.Views.Panes
{
    public sealed partial class Auxiliary : Page
    {
        private static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel), typeof(CommentsViewModel), typeof(Auxiliary), new PropertyMetadata(null));

        public CommentsViewModel ViewModel => (CommentsViewModel)GetValue(ViewModelProperty);

        public Auxiliary()
        {
            this.InitializeComponent();

            //var model = App.Container.Resolve<CommentsViewModel>();
            //SetValue(ViewModelProperty, model);
        }
    }
}