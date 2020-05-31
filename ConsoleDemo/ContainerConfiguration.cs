using Autofac;
using ConsoleDemoLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDemo
{
    public static class ContainerConfiguration
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<BusinessLogic>().As<IBusinessLogic>();

            builder.RegisterType<StartupApp>().As<IStartupApp>();

            builder.RegisterAssemblyTypes(Assembly.Load(nameof(ConsoleDemoLibrary)))
                .Where(type => type.Namespace.Contains("Methods"))
                .As(type => type.GetInterfaces().FirstOrDefault(i => i.Name == "I" + type.Name));

            return builder.Build();
        }
    }
}
