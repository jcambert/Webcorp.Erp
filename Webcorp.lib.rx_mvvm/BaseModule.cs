using Microsoft.Practices.ServiceLocation;
using Ninject;
using Prism.Events;
using Prism.Logging;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Webcorp.Model;

namespace Webcorp.rx_mvvm
{
    public class BaseModule : IModule, ILogger
    {

        private readonly string _myName;
        public BaseModule()
        {
            _myName = GetType().Name;
            /* this.Kernel = ServiceLocator.Current.GetInstance<IKernel>(); ;

             this.RegionManager = Kernel.Get<IRegionManager>();

             this.EventAggregator = Kernel.Get<IEventAggregator>();

             this.RegionNavigationService = Kernel.Get<IRegionNavigationService>();

             this.logger = this.Kernel.Get<ILoggerFacade>();*/
        }

        [Inject]
        public IKernel Kernel { get; set; }

        [Inject]
        public IEventAggregator EventAggregator { get; set; }

        [Inject]
        public IRegionManager RegionManager { get; set; }

        [Inject]
        public IRegionNavigationService RegionNavigationService { get; set; }

        public virtual void Initialize()
        {
            Debug("BaseModule Start initialize");
            Debug("BaseModule Start RegisterViewsWithModels");
            RegisterViewsWithModels();
            Debug("BaseModule End RegisterViewsWithModels");
            Debug("BaseModule Start RegisterViewsWithRegion");
            RegisterViewsWithRegion();
            Debug("BaseModule End RegisterViewsWithRegion");
            Debug("BaseModule Start RegisterMenus");
            RegisterMenus();
            Debug("BaseModule End RegisterMenus");
            Debug("BaseModule End initialize");
        }



        protected virtual void RegisterViewsWithRegion()
        {

        }

        protected virtual void RegisterViewsWithModels()
        {

        }

        protected virtual void RegisterMenus()
        {

        }

        protected void RegisterViewWithRegion(string viewName, Type type)
        {
            RegionManager.RegisterViewWithRegion(viewName, type);
        }

        protected void RegisterViewWithRegion<T>(string viewName)
        {
            RegisterViewWithRegion(viewName, typeof(T));
        }
        protected void RegisterViewWithModel<TVIEW, TIVIEWMODEL, TVIEWMODEL, TENTITY>() where TVIEW : FrameworkElement, new() where TIVIEWMODEL : IEntityViewModel<TENTITY> where TENTITY : IEntity where TVIEWMODEL : TIVIEWMODEL
        {
            Kernel.Bind(typeof(TIVIEWMODEL)).To(typeof(TVIEWMODEL));
            var t = new TVIEW();
            t.DataContext = Kernel.Get<TIVIEWMODEL>();

            Kernel.Bind<TVIEW>().ToMethod(ctx => t);
        }


        protected void RegisterMenu<TMENU, TVIEWMODEL, TENTITY>(string menuName, string ribbonRegion) where TMENU : FrameworkElement where TVIEWMODEL : IEntityViewModel<TENTITY> where TENTITY : IEntity
        {
            Kernel.Bind<TMENU>().ToSelf().InSingletonScope().Named(menuName);

            RegionManager.Regions[ribbonRegion].Add(Kernel.Get<TMENU>());

            Kernel.Get<TMENU>().DataContext = Kernel.Get<TVIEWMODEL>();
        }
        #region Logger

        public string MyName => _myName;
        [Inject]
        public ILoggerFacade Logger { get; set; }
        public void Debug(string message)
        {
            Logger.Log(MyName + "-" + message, Category.Debug, Priority.Low);
        }
        public void Info(string message)
        {
            Logger.Log(MyName + "-" + message, Category.Info, Priority.Low);
        }
        public void Warn(string message)
        {
            Logger.Log(MyName + "-" + message, Category.Warn, Priority.Medium);
        }
        public void Exception(string message)
        {
            Logger.Log(MyName + "-" + message, Category.Exception, Priority.High);
        }

        #endregion
    }
}
