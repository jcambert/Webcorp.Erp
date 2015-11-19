using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.rx_mvvm;

namespace Webcorp.erp.common
{
    public interface IShell 
    {
        IShellViewModel ShellViewModel { get; set; }
    }


    public interface IShellViewModel:IViewModel, IDataErrorInfo
    {
        List<AccentColorMenuData> AccentColors { get; set; }

        List<AppThemeMenuData> ApplicationThemes { get; set; }

        List<CultureInfo> CultureInfos { get; set; }

    }
}
