using Microsoft.Practices.ServiceLocation;
using Ninject;
using Prism.Events;
using Prism.Logging;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Webcorp.Model;

namespace Webcorp.rx_mvvm
{
    public class BaseModule : IModule, ILoggable
    {
        public event EventHandler<RegionNavigationFailedEventArgs> OnNavigationFailed = delegate { };
        public event EventHandler<RegionNavigationEventArgs> OnNavigating = delegate { };

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
        IServiceLocator ServiceLocator{get;set;}
        [Inject]
        public IEventAggregator EventAggregator { get; set; }
        [Inject]
        public IRegionManager RegionManager { get; set; }
        

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
            Debug("BaseModule Start RegisterRegionNavigationEvents");
            RegisterRegionNavigationEvents();
            Debug("BaseModule End RegisterRegionNavigationEvents");

            Debug("BaseModule End initialize");
        }

        protected virtual void Navigating(object sender, RegionNavigationEventArgs e)
        {
            Debug("Navigating to " + e.Uri.ToString());
            OnNavigating(sender, e);
        }

        protected virtual void NavigationFailed(object sender, RegionNavigationFailedEventArgs e)
        {
            Exception("Navigation Failed:"+e.Error.ToString());
            OnNavigationFailed(sender, e);
            
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

        protected virtual void RegisterRegionNavigationEvents()
        {

        }

        protected void RegisterViewWithRegion(string regionName, Type type)
        {
            RegionManager.RegisterViewWithRegion(regionName, type);
            
        }

        protected void RegisterViewWithRegion<T>(string regionName)
        {
            RegisterViewWithRegion(regionName, typeof(T));
            

        }

        protected void RegisterRegionNavigationEvent(string regionName)
        {
            RegionManager.Regions[regionName].NavigationService.NavigationFailed += NavigationFailed;
            RegionManager.Regions[regionName].NavigationService.Navigating += Navigating;
            //RegionManager.Regions[regionName].NavigationService.Navigated += Navigated;

        }


        protected void RegisterViewWithModel<TVIEW, TIVIEWMODEL, TVIEWMODEL, TENTITY>() where TVIEW : FrameworkElement, new() where TIVIEWMODEL : IEntityViewModel<TENTITY> where TENTITY : IEntity where TVIEWMODEL : TIVIEWMODEL
        {
            if(!Kernel.GetBindings(typeof(TIVIEWMODEL)).Any())
                Kernel.Bind(typeof(TIVIEWMODEL)).To(typeof(TVIEWMODEL)).InSingletonScope();
            
            var t = new TVIEW();
            t.DataContext = Kernel.Get<TIVIEWMODEL>();

            Kernel.Bind<TVIEW>().ToMethod(ctx => t);
        }

        protected void RegisterMenu<TMENU>(string menuName, string ribbonRegion) where TMENU : FrameworkElement 
        {
            Kernel.Bind<TMENU>().ToSelf().InSingletonScope().Named(menuName);

            RegionManager.Regions[ribbonRegion].Add(Kernel.Get<TMENU>());
        }
        protected void RegisterMenu<TMENU, TVIEWMODEL, TENTITY>(string menuName, string ribbonRegion) where TMENU : FrameworkElement where TVIEWMODEL : IViewModel
        {
            Kernel.Bind<TMENU>().ToSelf().InSingletonScope().Named(menuName);

            RegionManager.Regions[ribbonRegion].Add(Kernel.Get<TMENU>());

            Kernel.Get<TMENU>().DataContext = Kernel.Get<TVIEWMODEL>();
        }
        #region Logger

        public string MyName => _myName;
        [Inject]
        public ILogger Logger { get; set; }
        public void Debug(string message, [CallerMemberName] string caller = "")
        {
            Logger.Debug(message,caller);
        }
        public void Debug([CallerMemberName] string message="")
        {
            Logger.Debug(message);
        }
        public void Info(string message, [CallerMemberName] string caller = "")
        {
            Logger.Info(message, caller);
        }
        public void Info([CallerMemberName] string message = "")
        {
            Logger.Info(message);
        }
        public void Warn(string message, [CallerMemberName] string caller = "")
        {
            Logger.Warn(message, caller);
        }
        public void Warn([CallerMemberName] string message = "")
        {
            Logger.Warn(message);
        }
        public void Exception(string message, [CallerMemberName] string caller = "")
        {
            Logger.Exception(message, caller);
        }
        public void Exception([CallerMemberName] string message = "")
        {
            Logger.Exception(message);
        }
        #endregion
    }
}
