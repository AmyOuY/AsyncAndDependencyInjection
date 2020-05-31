using ConsoleDemoLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDemo
{
    public class StartupApp : IStartupApp
    {
        private readonly IBusinessLogic _businessLogic;

        public StartupApp(IBusinessLogic businessLogic)
        {
            _businessLogic = businessLogic;
        }


        public void Run()
        {
            _businessLogic.ProcessData();
        }
    }
}
