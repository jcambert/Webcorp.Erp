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
using Webcorp.Dal;
using Webcorp.Model;

namespace Webcorp.erp
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IKernel container;
        [Inject]
        public MainWindow(IKernel kernel)
        {
            this.container = kernel;
            InitializeComponent();

        }


        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            var repo = container.Get<IRepository<Material>>();
            var mp = container.Get<IEntityProvider<Material, string>>();
        }
    }
}
