using Ninject;
using Ninject.Modules;
using Ninject.Extensions.Conventions;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Controller;
using Webcorp.erp.common;
using Webcorp.erp.Views;
using Webcorp.Model;
using Webcorp.Model.Quotation;
using Webcorp.reactive;

namespace Webcorp.erp
{
    [Module(ModuleName = "ErpApplicationModule")]
    public class ErpApplicationModule :BaseModule
    {
        public ErpApplicationModule():base()
        {
           
        }
        public override void Initialize()
        {
            base.Initialize();

            
            

            Kernel.Bind(typeof(IEntityController<>)).To(typeof(BusinessEntityController<>));
            Kernel.Bind(typeof(IBusinessControllerProvider<>)).To(typeof(BusinessControllerProvider<>)).InSingletonScope();
            Kernel.Bind(typeof(IBusinessAssemblyProvider)).To(typeof(BusinessAssemblyProvider)).InSingletonScope();
            Kernel.Bind(typeof(IEntityProvider<,>)).To(typeof(EntityProvider<,>)).InSingletonScope();
            Kernel.Bind(typeof(IEntityProviderInitializable<VitesseDecoupeLaser, string>)).To(typeof(VitesseDecoupeLaserInitializer));
            Kernel.Bind(typeof(IEntityProviderInitializable<PosteCharge, int>)).To(typeof(PosteChargeInitializer));
            Kernel.Bind(typeof(IEntityProviderInitializable<MaterialPrice, string>)).To(typeof(MaterialPriceInitializer));
            Kernel.Bind(typeof(IEntityProviderInitializable<Material, string>)).To(typeof(MaterialInitializer));
        }
        protected override void RegisterViewsWithRegion()
        {
            base.RegisterViewsWithRegion();
            RegisterViewWithRegion<home>(Regions.Client);
            
        }

        protected override void RegisterMenus()
        {
            base.RegisterMenus();
            RegisterMenu<ApplicationMenu>("fichier", Regions.Ribbon);
        }

        protected override void RegisterRegionNavigationEvents()
        {
            base.RegisterRegionNavigationEvents();
            RegisterRegionNavigationEvent(Regions.Client);
           
        }
    }
}
