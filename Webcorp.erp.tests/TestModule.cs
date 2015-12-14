using System;
using Webcorp.Business;
using Webcorp.Controller;
using Webcorp.Model;
using Webcorp.Model.Quotation;

namespace Webcorp.erp.tests
{
    public class TestModule : Ninject.Modules.NinjectModule
    {
        public TestModule()
        {
        }

        public override void Load()
        {
            // Bind(typeof(IEntityController<>)).To(typeof(EntityController<>));
            //Bind(typeof(IBusinessControllerProvider<>)).To(typeof(BusinessControllerProvider<>)).InSingletonScope();
            //Bind(typeof(IBusinessControllerAssemblyProvider)).To(typeof(BusinessControllerAssemblyProvider)).InSingletonScope();
            //Bind(typeof(IBusinessProvider<>)).To(typeof(BusinessProvider<>)).InSingletonScope();
            //Bind(typeof(IBusinessAssemblyProvider)).To(typeof(BusinessAssemblyProvider)).InSingletonScope();
            //Bind(typeof(IBusinessHelper<>)).To(typeof(BusinessHelper<>)).InSingletonScope();
            Bind(typeof(IArticleBusinessController<>)).To(typeof(ArticleBusinessController<>));

            Bind(typeof(IEntityProvider<,>)).To(typeof(EntityProvider<,>)).InSingletonScope();
            Bind(typeof(IEntityProviderInitializable<VitesseDecoupeLaser, string>)).To(typeof(VitesseDecoupeLaserInitializer));
            Bind(typeof(IEntityProviderInitializable<PosteCharge, int>)).To(typeof(PosteChargeInitializer));
            Bind(typeof(IEntityProviderInitializable<MaterialPrice, string>)).To(typeof(MaterialPriceInitializer));
            Bind(typeof(IEntityProviderInitializable<Material, string>)).To(typeof(MaterialInitializer));
        }
    }
}