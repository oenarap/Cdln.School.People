using System;
using Autofac;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Cdln.School.People.Uwp.Lists;

namespace Cdln.School.People.Uwp.Views.Panes
{
    public sealed partial class PeopleList : Page
    {
        private static readonly DependencyProperty PeopleListProviderProperty = DependencyProperty.Register(nameof(PeopleListProvider), typeof(PeopleListProvider), typeof(PeopleList), new PropertyMetadata(null));
        private static readonly DependencyProperty ContextProviderProperty = DependencyProperty.Register(nameof(ContextProvider), typeof(PeopleContextsProvider), typeof(PeopleList), new PropertyMetadata(null));

        public PeopleListProvider PeopleListProvider => (PeopleListProvider)GetValue(PeopleListProviderProperty);
        public PeopleContextsProvider ContextProvider => (PeopleContextsProvider)GetValue(ContextProviderProperty);

        public PeopleList()
        {
            this.InitializeComponent();

            SetValue(ContextProviderProperty, App.Container.Resolve<PeopleContextsProvider>() ?? throw new NullReferenceException());
            SetValue(PeopleListProviderProperty, App.Container.Resolve<PeopleListProvider>() ?? throw new NullReferenceException());
        }

        private void OnPeopleListItemClicked(object sender, ItemClickEventArgs e)
        {
            // TODO:
        }
    }
}
