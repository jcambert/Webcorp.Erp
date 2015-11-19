using Prism.Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Webcorp.Dal;
using System.Windows;
using Prism.Modularity;
using Prism.Regions;

using Prism.Events;
using System.Windows.Controls.Ribbon;
using Webcorp.erp.Utilities;
using Microsoft.Practices.ServiceLocation;
using Ninject.Modules;
using Webcorp.erp.common;
using MahApps.Metro.Controls;
using Webcorp.erp.article;
#if DEBUG
using Webcorp.erp.quotation;
#endif
namespace Webcorp.erp
{
    public class ErpApplicationBootstrapper : NinjectBootstrapper
    {

        private Func<INinjectModule[]> NinjectModules;


        private INinjectModule[] InternalNinjectModules()
        {
            List<INinjectModule> modules = new List<INinjectModule>();
            modules.Add(new InternalNinjectModule());

            return modules.ToArray();
        }

        public override void Run(bool runWithDefaultConfiguration)
        {
            NinjectModules = InternalNinjectModules;
            base.Run(runWithDefaultConfiguration);
        }

        public IModuleManager ModuleManager { get; private set; }

        //protected override DependencyObject CreateShell() => Kernel.Get<MainWindow>();
        protected override DependencyObject CreateShell() => Kernel.Get<MainMetroWindow>();

        protected override IKernel CreateKernel()
        {

            IKernel kernel = new StandardKernel(NinjectModules());
            return kernel;
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.MainWindow = (Window)this.Shell;

            Application.Current.MainWindow.Show();
        }

        protected override IModuleCatalog CreateModuleCatalog() => new AggregateModuleCatalog();// new DirectoryModuleCatalog() {ModulePath= @".\modules" };


        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();




            //Configuration Modules Declaration
            ConfigurationModuleCatalog configurationCatalog = new ConfigurationModuleCatalog();
            ((AggregateModuleCatalog)ModuleCatalog).AddCatalog(configurationCatalog);

            var erpmodtype = typeof(ErpApplicationModule);
            ModuleCatalog.AddModule(new ModuleInfo(erpmodtype.Name, erpmodtype.AssemblyQualifiedName));
#if DEBUG

            Type quotmodType = typeof(QuotationModule);
            ModuleCatalog.AddModule(new ModuleInfo(quotmodType.Name, quotmodType.AssemblyQualifiedName));

            Type artmodType = typeof(ArticleModule);
            ModuleCatalog.AddModule(new ModuleInfo(artmodType.Name, artmodType.AssemblyQualifiedName));
#else
            //Directory Modules dll 
            DirectoryModuleCatalog directoryCatalog = new DirectoryModuleCatalog() { ModulePath = @".\modules" };
            ((AggregateModuleCatalog)ModuleCatalog).AddCatalog(directoryCatalog);
#endif
            //this.ModuleManager= ServiceLocator.Current.GetInstance<IModuleManager>();
        }

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            var mappings = base.ConfigureRegionAdapterMappings();
            if (mappings == null) return null;

            // Add custom mappings
            mappings.RegisterMapping(typeof(Ribbon), ServiceLocator.Current.GetInstance<RibbonRegionAdapter>());
            mappings.RegisterMapping(typeof(MetroAnimatedSingleRowTabControl), ServiceLocator.Current.GetInstance<MetroAnimatedSingleRowTabControlAdapter>());
            return mappings;
        }

        protected override void ConfigureKernel()
        {
            base.ConfigureKernel();

            Kernel.Load("*.dll");
        }


    }

    public class InternalNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IShellViewModel>().To<MainWindowViewModel>();
        }
    }
}
