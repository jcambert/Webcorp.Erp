using Microsoft.Practices.ServiceLocation;
using Ninject;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
#if DEBUG
using System.Diagnostics;
#endif
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Webcorp.reactive
{
    public class BaseModule : IModule, ILoggable
    {
        public event EventHandler<RegionNavigationFailedEventArgs> OnNavigationFailed = delegate { };
        public event EventHandler<RegionNavigationEventArgs> OnNavigating = delegate { };

        private readonly string _myName;
        public BaseModule()
        {
            _myName = GetType().Name;

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
#if DEBUG
            if (Debugger.IsAttached) Debugger.Break();
#endif
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
            try
            {
                RegionManager.Regions[regionName].NavigationService.NavigationFailed += NavigationFailed;
                RegionManager.Regions[regionName].NavigationService.Navigating += Navigating;
                //RegionManager.Regions[regionName].NavigationService.Navigated += Navigated;
            }
            catch (KeyNotFoundException) { }

        }


       /* protected void RegisterViewWithModel<TVIEW, TIVIEWMODEL, TVIEWMODEL, TENTITY>() where TVIEW : FrameworkElement, new() where TIVIEWMODEL : IEntityViewModel<TENTITY> where TENTITY : IEntity where TVIEWMODEL : TIVIEWMODEL
        {
            if(!Kernel.GetBindings(typeof(TIVIEWMODEL)).Any())
                Kernel.Bind(typeof(TIVIEWMODEL)).To(typeof(TVIEWMODEL)).InSingletonScope();
            if (!Kernel.GetBindings(typeof(TVIEW)).Any())
                Kernel.Bind<TVIEW>().ToSelf().OnActivation(v=>v.DataContext=Kernel.Get<TIVIEWMODEL>());
            
        }*/

        protected void RegisterViewWithModel<TVIEW, TVIEWMODEL>(bool asSingleton = true) where TVIEW : FrameworkElement
        {
            if (asSingleton)
                if (Kernel.GetBindings(typeof(TVIEWMODEL)).Count() == 0)
                    Kernel.Bind<TVIEWMODEL>().ToSelf().InSingletonScope();

            if (!asSingleton)
                Kernel.Bind<TVIEWMODEL>().ToSelf();

            Kernel.Bind<TVIEW>().ToSelf().OnActivation(v => v.DataContext = Kernel.Get<TVIEWMODEL>());
        }

        protected void RegisterMenu<TMENU>(string menuName, string ribbonRegion) where TMENU : FrameworkElement 
        {
            Kernel.Bind<TMENU>().ToSelf().InSingletonScope().Named(menuName);
            try {
                RegionManager.Regions[ribbonRegion].Add(Kernel.Get<TMENU>());
            }
            catch (KeyNotFoundException)
            {

            }
        }
        protected void RegisterMenu<TMENU, TVIEWMODEL, TENTITY>(string menuName, string ribbonRegion) where TMENU : FrameworkElement //where TVIEWMODEL : IViewModel
        {
            Kernel.Bind<TMENU>().ToSelf().InSingletonScope().Named(menuName);
            try {
                RegionManager.Regions[ribbonRegion].Add(Kernel.Get<TMENU>());
            }
            catch (KeyNotFoundException) { }
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
