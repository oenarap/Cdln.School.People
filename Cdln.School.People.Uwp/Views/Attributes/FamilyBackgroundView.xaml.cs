using System;
using Autofac;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Cdln.School.People.Uwp.ViewModels.Attributes;

namespace Cdln.School.People.Uwp.Views.Attributes
{
    public sealed partial class FamilyBackgroundView : Page
    {
        private static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel), typeof(FamilyBackgroundViewModel), typeof(FamilyBackgroundView), new PropertyMetadata(null));

        public FamilyBackgroundViewModel ViewModel => (FamilyBackgroundViewModel)GetValue(ViewModelProperty);

        public FamilyBackgroundView()
        {
            this.InitializeComponent();

            SetValue(ViewModelProperty, App.Container.Resolve<FamilyBackgroundViewModel>() ?? throw new NullReferenceException());
        }
    }
}
