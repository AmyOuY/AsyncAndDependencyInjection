using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDemo.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
    
       public void Download()
        {
            ActivateItem(IoC.Get<DownloadViewModel>());
        }
    }
}
