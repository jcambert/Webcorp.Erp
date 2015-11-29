using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace Webcorp.erp.common
{
    public interface IShell 
    {
        IShellViewModel ShellViewModel { get; set; }
    }


    public interface IShellViewModel: IDataErrorInfo
    {
        List<AccentColorMenuData> AccentColors { get; set; }

        List<AppThemeMenuData> ApplicationThemes { get; set; }

        List<CultureInfo> CultureInfos { get; set; }

    }

    
}
