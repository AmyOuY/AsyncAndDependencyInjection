using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = ContainerConfiguration.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IStartupApp>();

                app.Run();
            }

            Console.ReadLine();
        }
    }
}
