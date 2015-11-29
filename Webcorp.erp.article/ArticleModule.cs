using Prism.Modularity;
using Webcorp.erp.article.ViewModel.impl;
using Webcorp.erp.article.Views;
using Webcorp.erp.common;
using Webcorp.Model.Article;
using Ninject;
using Webcorp.reactive;

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
           // RegisterViewWithModel<ArticleSummaryView, IArticleViewModel, ArticleViewModel, Article>();
            Kernel.Bind<ArticleViewModel>().ToSelf().InSingletonScope();
            Kernel.Bind<ArticleSummaryView>().ToSelf().OnActivation(v => v.DataContext = Kernel.Get<ArticleViewModel>());

        }

        protected override void RegisterMenus()
        {
            base.RegisterMenus();
            
            RegisterMenu<ArticleMetroMenu, ArticleViewModel, Article>("ArticleMenu", Regions.MainTab);
        }
    }
}
