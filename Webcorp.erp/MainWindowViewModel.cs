using MahApps.Metro;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using Webcorp.erp.common;
using System.Windows.Media;

namespace Webcorp.erp
{
    public class MainWindowViewModel : IMainWindowViewModel
    {

        public MainWindowViewModel()
        {
            

        }
        public string this[string columnName]=> null;
       

        public List<AccentColorMenuData> AccentColors
        {
            get;

            set;
        }

        public List<AppThemeMenuData> ApplicationThemes
        {
            get;

            set;
        }

        public List<CultureInfo> CultureInfos
        {
            get;

            set;
        }

        public string Error => string.Empty;

        public bool KeepAlive => true;

        public event PropertyChangedEventHandler PropertyChanged;

        public void Dispose()
        {
            
        }

        public void Initialize()
        {
            this.AccentColors = ThemeManager.Accents
                                             .Select(a => new AccentColorMenuData() { Name = a.Name, ColorBrush = a.Resources["AccentColorBrush"] as Brush })
                                             .ToList();
            this.ApplicationThemes = ThemeManager.AppThemes
                                          .Select(a => new AppThemeMenuData() { Name = a.Name, BorderColorBrush =a.Resources["BlackColorBrush"] as Brush, ColorBrush = a.Resources["WhiteColorBrush"] as Brush })
                                          .ToList();

            CultureInfos = CultureInfo.GetCultures(CultureTypes.InstalledWin32Cultures).ToList();
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
           
        }

        public void ShouldDispose(IDisposable disposable)
        {
            
        }
    }
}