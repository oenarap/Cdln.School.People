using Autofac;

namespace Cdln.School.People.Uwp
{
    internal static class Ioc
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            
            

            return builder.Build();
        }
    }
}
