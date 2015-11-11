using Microsoft.Practices.ServiceLocation;
using Ninject;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Webcorp.erp.common;
using Webcorp.erp.quotation.ViewModel;
using Webcorp.erp.quotation.ViewModel.impl;
using Webcorp.erp.quotation.Views;
using Webcorp.Model;
using Webcorp.Model.Quotation;
using Webcorp.rx_mvvm;

namespace Webcorp.erp.quotation
{
    [Module(ModuleName = "QuotationModule")]
    [ModuleDependency("ErpApplicationModule")]
    public class QuotationModule : BaseModule
    {


        public QuotationModule():base()
        {
           
        }
        

        protected override void RegisterViewsWithRegion()
        {
            base.RegisterViewsWithRegion();
            RegisterViewWithRegion<QuotationSummaryView>(Regions.Client);
        }

        protected override void RegisterViewsWithModels()
        {
            base.RegisterViewsWithModels();
            RegisterViewWithModel<QuotationSummaryView, IQuotationViewModel, QuotationViewModel, Quotation>();
        }

        protected override void RegisterMenus()
        {
            base.RegisterMenus();
            RegisterMenu<QuotationMenu, QuotationViewModel, Quotation>("QuotationMenu", Regions.Ribbon);
        }
    }
}
