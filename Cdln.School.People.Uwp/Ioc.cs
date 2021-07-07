using Autofac;
using School.People.Core;
using Cdln.School.People.Uwp.Data;
using Cdln.School.People.Uwp.Utils;
using Cdln.School.People.Uwp.Views;
using Cdln.School.People.Uwp.Lists;
using School.People.Core.Repositories;
using Cdln.School.People.Uwp.ViewModels;
using Cdln.School.People.Uwp.Views.Panes;
using Cdln.School.People.Uwp.ViewModels.Attributes;
using Cdln.School.People.Uwp.ViewModels.Auxiliaries;

namespace Cdln.School.People.Uwp
{
    internal static class Ioc
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            // api client
            builder.RegisterType<ApiClient>().As<IApiClient>().SingleInstance();

            // view models 
            builder.RegisterType<PeopleListViewModel>().SingleInstance();
            builder.RegisterType<AttributePaneViewModel>().SingleInstance();
            builder.RegisterType<ConspectusViewModel>().SingleInstance();
            builder.RegisterType<AuxiliaryViewModel>().SingleInstance();
            builder.RegisterType<FamilyBackgroundViewModel>().SingleInstance();

            // utilities
            builder.RegisterType<MessageHub>().As<IMessageHub>().SingleInstance();
            builder.RegisterType<IndexLogger>().As<IIndexLogger>().SingleInstance();
            builder.RegisterType<PeopleList>().SingleInstance();
            builder.RegisterType<Conspectus>().SingleInstance();
            builder.RegisterType<AttributePane>().SingleInstance();
            builder.RegisterType<Auxiliary>().SingleInstance();
            builder.RegisterType<AddPersonDialog>().SingleInstance();

            // ui data & local repos
            builder.RegisterType<PeopleListProvider>().SingleInstance();
            builder.RegisterType<PeopleContextsProvider>().SingleInstance();
            builder.RegisterType<AttributeContextsProvider>().SingleInstance();
            builder.RegisterType<CommentsRepository>().As<ICommentsRepository>().SingleInstance();


            return builder.Build();
        }
    }
}
