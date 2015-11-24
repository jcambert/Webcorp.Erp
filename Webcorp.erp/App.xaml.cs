using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Webcorp.Dal;
using Webcorp.Model;

namespace Webcorp.erp
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
      
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            var bootstrapper = new ErpApplicationBootstrapper();
            bootstrapper.Run(true);
            Application.Current.MainWindow.WindowState = WindowState.Maximized;
        }

       
    }
}
