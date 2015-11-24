using MahApps.Metro.Controls;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Webcorp.erp.common;

namespace Webcorp.erp
{
    /// <summary>
    /// Logique d'interaction pour MainMetroWindow.xaml
    /// </summary>
    public partial class MainMetroWindow : MetroWindow,IShell
    {
        public MainMetroWindow()
        {
            InitializeComponent();
        }

        [Inject]
        public IShellViewModel ShellViewModel
        {
            get { return DataContext as IShellViewModel; }
            set { DataContext = value; }
        }

        private  void CloseCustomDialog(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
