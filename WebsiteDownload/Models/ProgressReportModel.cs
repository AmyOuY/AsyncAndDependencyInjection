using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDemo.Library.Models
{
    public class ProgressReportModel
    {
        public int PercentageCompleted { get; set; }

        public List<WebsiteDataModel> Downloaded { get; set; } = new List<WebsiteDataModel>();
    }
}
