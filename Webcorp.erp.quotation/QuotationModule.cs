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
          /*  RegisterViewWithRegion<QuotationSummaryView>(Regions.Client);
            RegisterViewWithRegion<QuotationDetailView>(Regions.Client);
            RegisterViewWithRegion<QuotationFormView>(Regions.Client);*/

            RegisterViewWithRegion<QuotationSummaryView>(QuotationRegions.Main);
            //RegisterViewWithRegion<QuotationDetailView>(QuotationRegions.Main);
            RegisterViewWithRegion<QuotationFormView>(QuotationRegions.Main);
        }

        protected override void RegisterViewsWithModels()
        {
            base.RegisterViewsWithModels();
            /* RegisterViewWithModel<QuotationSummaryView, IQuotationViewModel, QuotationViewModel, Quotation>();
             RegisterViewWithModel<QuotationDetailView, IQuotationViewModel, QuotationViewModel, Quotation>();
             RegisterViewWithModel<QuotationFormView, IQuotationViewModel, QuotationViewModel, Quotation>();*/

            Kernel.Bind<QuotationReactiveViewModel>().ToSelf().InSingletonScope(); ;
            Kernel.Bind<QuotationSummaryView>().ToSelf().OnActivation(v => v.DataContext = Kernel.Get<QuotationReactiveViewModel>());
            Kernel.Bind<QuotationFormView>().ToSelf().OnActivation(v => v.DataContext = Kernel.Get<QuotationReactiveViewModel>());
        }

        protected override void RegisterMenus()
        {
            base.RegisterMenus();
            //RegisterMenu<QuotationMenu, QuotationViewModel, Quotation>("QuotationMenu", Regions.Ribbon);
            RegisterMenu<QuotationMetroMenu, QuotationViewModel, Quotation>("QuotationMenu", Regions.MainTab);
        }

        protected override void RegisterRegionNavigationEvents()
        {
            base.RegisterRegionNavigationEvents();
            RegisterRegionNavigationEvent(QuotationRegions.Main);
        }
    }
}
