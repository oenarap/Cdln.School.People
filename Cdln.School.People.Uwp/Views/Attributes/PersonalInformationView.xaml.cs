using System;
using Autofac;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Cdln.School.People.Uwp.Lists;
using Cdln.School.People.Uwp.ViewModels;

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
    }
}
