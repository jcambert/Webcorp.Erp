﻿using System;
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

namespace Webcorp.erp.quotation.Views
{
    /// <summary>
    /// Logique d'interaction pour QuotationListeArticleView.xaml
    /// </summary>
    public partial class QuotationListeArticleView : UserControl
    {
        public QuotationListeArticleView()
        {
            InitializeComponent();
        }

        public object MyDataContext { get; set; }
    }
}