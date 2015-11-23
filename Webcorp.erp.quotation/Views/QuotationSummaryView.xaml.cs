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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Webcorp.erp.quotation.ViewModel.impl;

namespace Webcorp.erp.quotation.Views
{
    /// <summary>
    /// Logique d'interaction pour QuaotationSummaryView.xaml
    /// </summary>
    public partial class QuotationSummaryView : UserControl
    {
        public QuotationSummaryView()
        {
            InitializeComponent();
            
        }

        private void OnChecked(object sender, RoutedEventArgs e)
        {
            
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as QuotationViewModel;
            foreach (var item in vm.Models)
            {
                item.IsSelected = true;
                item.OnPropertyChanged("IsSelected");
            }
        }
    }
}
