using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Webcorp.erp.quotation.Views
{
    /// <summary>
    /// Logique d'interaction pour QuotationDetailView.xaml
    /// </summary>
    public partial class QuotationDetailView : UserControl
    {
        public QuotationDetailView()
        {
            InitializeComponent();
            Parameters = new ObservableCollection<FormElement>();
            Parameters.Add(new StringElement() { Name = "Texte " + Parameters.Count,Value="test" });
            DataContext = this;
        }

        public ObservableCollection<FormElement> Parameters { get; set; }

    }
}
