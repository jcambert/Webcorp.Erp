using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webcorp.Business;
using Webcorp.Model;
using Webcorp.Model.Quotation;

namespace Webcorp.erp.rest.Ioc
{
    public class ErpRestModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IArticleBusinessController<>)).To(typeof(ArticleBusinessController<>));

            Bind(typeof(IEntityProvider<,>)).To(typeof(EntityProvider<,>)).InSingletonScope();
            Bind(typeof(IEntityProviderInitializable<Article, string>)).To(typeof(BeamInitializer));
            Bind(typeof(IEntityProviderInitializable<VitesseDecoupeLaser, string>)).To(typeof(VitesseDecoupeLaserInitializer));
            Bind(typeof(IEntityProviderInitializable<PosteCharge, int>)).To(typeof(PosteChargeInitializer));
            Bind(typeof(IEntityProviderInitializable<MaterialPrice, string>)).To(typeof(MaterialPriceInitializer));
            Bind(typeof(IEntityProviderInitializable<Material, string>)).To(typeof(MaterialInitializer));
        }
    }
}