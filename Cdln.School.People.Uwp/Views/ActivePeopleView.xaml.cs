using Autofac;
using Cdln.School.People.Uwp.Models;
using Cdln.School.People.Uwp.Views.Panes;
using School.People.Uwp.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Cdln.School.People.Uwp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ActivePeopleView : Page
    {
        public readonly List<Person> People;
        public readonly List<Page> Panes;

        public ActivePeopleView()
        {
            this.InitializeComponent();

            People = new List<Person>()
            {
                new Person(Guid.NewGuid(), "Potter", "Harry James", "Bond"),
                new Person(Guid.NewGuid(), "Wesley", "Ronald Bilius", "Snipes"),
                new Person(Guid.NewGuid(), "Granger", "Hermione Jean", "Grey"),
                new Person(Guid.NewGuid(), "Snape", "Severus", "Prince"),
                new Person(Guid.NewGuid(), "Dumbledore", "Albus Wulfric Brian", "Adams"),
                new Person(Guid.NewGuid(), "Malfoy", "Draco", "Bachtin"),
                new Person(Guid.NewGuid(), "Longbottom", "Neville", "Aaron")
            };

            AdaptiveBladeView.SetProminence(People[2], BladeProminence.Prominent);

            var people = App.Container.Resolve<PeopleList>();
            var conspectus = App.Container.Resolve<Conspectus>();
            var attributes = App.Container.Resolve<AttributePane>();
            var auxiliary = App.Container.Resolve<Auxiliary>();

            Panes = new List<Page>()
            {
                people,
                conspectus,
                attributes,
                auxiliary
            };

            AdaptiveBladeView.SetProminence(Panes[2], BladeProminence.Prominent);
        }
    }
}
