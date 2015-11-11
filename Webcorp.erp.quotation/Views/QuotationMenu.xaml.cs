using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Webcorp.erp.quotation.Views
{
    /// <summary>
    /// Logique d'interaction pour QuotationMenu.xaml
    /// </summary>
    public partial class QuotationMenu : RibbonTab, IRegionMemberLifetime
    {
        public QuotationMenu()
        {
            InitializeComponent();
        }

        public bool KeepAlive
        {
            get
            {
                return true;
            }
        }
    }
}
