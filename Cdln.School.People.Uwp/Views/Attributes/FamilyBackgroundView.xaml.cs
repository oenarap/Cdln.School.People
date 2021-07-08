using System;
using Autofac;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Cdln.School.People.Uwp.ViewModels.Attributes;
using School.People.Core;
using Windows.UI.Xaml.Navigation;

namespace Cdln.School.People.Uwp.Views.Attributes
{
    public sealed partial class FamilyBackgroundView : Page
    {
        private static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel), typeof(FamilyBackgroundViewModel), typeof(FamilyBackgroundView), new PropertyMetadata(null));

        public FamilyBackgroundViewModel ViewModel => (FamilyBackgroundViewModel)GetValue(ViewModelProperty);

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is IPerson person)
            {
                // TODO: request for data
            }
            else
            {
                // TODO: reset content & lock the page for editing
            }

            base.OnNavigatedTo(e);
        }

        public FamilyBackgroundView()
        {
            this.InitializeComponent();

            SetValue(ViewModelProperty, App.Container.Resolve<FamilyBackgroundViewModel>() ?? throw new NullReferenceException());
        }
    }
}
