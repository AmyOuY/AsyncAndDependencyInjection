using ConsoleDemoLibrary.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDemoLibrary
{
    public class BusinessLogic : IBusinessLogic
    {
        private readonly IDataAccess _dataAccess;
        private readonly ILogger _logger;

        public BusinessLogic(IDataAccess dataAccess, ILogger logger)
        {
            _dataAccess = dataAccess;
            _logger = logger;
        }


        public void ProcessData()
        {
            _logger.Log("Starting processing data");
            Console.WriteLine("Processing data");
            _dataAccess.LoadData();
            _dataAccess.SaveData("DataInfo");
            _logger.Log("Finished processing data");
        }
    }
}
