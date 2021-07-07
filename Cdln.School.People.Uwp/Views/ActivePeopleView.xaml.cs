using System;
using Autofac;
using Windows.UI.Xaml.Controls;
using School.People.Uwp.Controls;
using Windows.UI.Xaml.Navigation;
using Cdln.School.People.Uwp.Lists;
using Cdln.School.People.Uwp.Models;
using Cdln.School.People.Uwp.Views.Panes;

namespace Cdln.School.People.Uwp.Views
{
    public sealed partial class ActivePeopleView : Page
    {
        private PeopleContextDescriptor peopleContext;
        public readonly Frame[] Frames;

        public ActivePeopleView()
        {
            this.InitializeComponent();

            var peopleList = new Frame();
            var conspectus = new Frame();
            var attributes = new Frame();
            var auxiliary = new Frame();

            AdaptiveBladeView.SetProminence(attributes, BladeProminence.Prominent);
            Frames = new Frame[4] { peopleList, conspectus, attributes, auxiliary }; // Note: the order matters
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is NavigationContext navContext)
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    peopleContext = navContext.GetInstance<PeopleContextDescriptor>();

                    if (peopleContext != null)
                    {
                        var peopleProvider = App.Container.Resolve<PeopleListProvider>() ?? throw new NullReferenceException();
                        var attributeContexts = App.Container.Resolve<AttributeContextsProvider>() ?? throw new NullReferenceException();

                        navContext.Add(peopleProvider);
                        navContext.Add(attributeContexts);

                        Frames[0].Navigate(typeof(PeopleList), navContext);
                        Frames[1].Navigate(typeof(Conspectus), navContext);
                        Frames[3].Navigate(typeof(Auxiliary), navContext);
                        Frames[2].Navigate(typeof(AttributePane), navContext);
                    }
                    else
                    {
                        navContext.Root?.Navigate(typeof(Blank));
                    }
                });
            }
        }
    }
}
