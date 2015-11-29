using System;
using Ninject;
using Prism.Modularity;
using Webcorp.erp.common;
using Webcorp.erp.quotation.ViewModel.impl;
using Webcorp.erp.quotation.Views;
using Webcorp.Model.Quotation;
using Webcorp.reactive;
using System.Linq;
using System.Windows;

namespace Webcorp.erp.quotation
{
    [Module(ModuleName = "QuotationModule")]
    [ModuleDependency("ErpApplicationModule")]
    public class QuotationModule : BaseModule
    {


        public QuotationModule() : base()
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
            RegisterViewWithRegion<QuotationListeArticleView>(QuotationRegions.ListeArticle);
            RegisterViewWithRegion<QuotationArticleSummaryView>(QuotationRegions.SummaryArticle);
        }

        protected override void RegisterViewsWithModels()
        {
            base.RegisterViewsWithModels();
            RegisterViewWithModel<QuotationSummaryView, QuotationReactiveViewModel>();
            RegisterViewWithModel<QuotationFormView, QuotationReactiveViewModel>();
        }



        protected override void RegisterMenus()
        {
            base.RegisterMenus();
            //RegisterMenu<QuotationMenu, QuotationViewModel, Quotation>("QuotationMenu", Regions.Ribbon);
            RegisterMenu<QuotationMetroMenu, QuotationReactiveViewModel, Quotation>("QuotationMenu", Regions.MainTab);
        }

        protected override void RegisterRegionNavigationEvents()
        {
            base.RegisterRegionNavigationEvents();
            RegisterRegionNavigationEvent(QuotationRegions.Main);
        }
    }
}
