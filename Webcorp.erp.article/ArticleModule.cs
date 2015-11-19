using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.erp.article.ViewModel;
using Webcorp.erp.article.ViewModel.impl;
using Webcorp.erp.article.Views;
using Webcorp.erp.common;
using Webcorp.Model.Article;
using Webcorp.rx_mvvm;

namespace Webcorp.erp.article
{
    [Module(ModuleName = "ArticleModule")]
    [ModuleDependency("ErpApplicationModule")]
    public class ArticleModule:BaseModule
    {
        protected override void RegisterViewsWithRegion()
        {
            base.RegisterViewsWithRegion();
            RegisterViewWithRegion<ArticleSummaryView>(Regions.Client);

            RegisterViewWithRegion<ArticleSummaryView>(ArticleRegions.Main);
        }

        protected override void RegisterViewsWithModels()
        {
            base.RegisterViewsWithModels();
            RegisterViewWithModel<ArticleSummaryView, IArticleViewModel, ArticleViewModel, Article>();

        }

        protected override void RegisterMenus()
        {
            base.RegisterMenus();
            
            RegisterMenu<ArticleMetroMenu, ArticleViewModel, Article>("ArticleMenu", Regions.MainTab);
        }
    }
}
