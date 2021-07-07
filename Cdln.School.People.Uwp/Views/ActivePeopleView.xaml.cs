using System;
using Autofac;
using Windows.UI.Xaml.Controls;
using School.People.Uwp.Controls;
using System.Collections.Generic;
using Cdln.School.People.Uwp.Views.Panes;

namespace Cdln.School.People.Uwp.Views
{
    public sealed partial class ActivePeopleView : Page
    {
        public readonly List<Page> Panes;

        public ActivePeopleView()
        {
            this.InitializeComponent();

            var peopleList = App.Container.Resolve<PeopleList>() ?? throw new NullReferenceException();
            var conspectus = App.Container.Resolve<Conspectus>() ?? throw new NullReferenceException();
            var attributePane = App.Container.Resolve<AttributePane>() ?? throw new NullReferenceException();
            var auxiliary = App.Container.Resolve<Auxiliary>() ?? throw new NullReferenceException();
            
            AdaptiveBladeView.SetProminence(attributePane, BladeProminence.Prominent);
            Panes = new List<Page>() { peopleList, conspectus, attributePane, auxiliary };
        }
    }
}
