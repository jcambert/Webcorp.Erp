using Ninject;
using Prism.Regions;
using System;
namespace Webcorp.reactive
{
    public class ReactiveViewModel<T> : ReactiveVMCollection<T>, IReactiveViewModel<T>, IRegionMemberLifetime where T : class
    {
       

       
        public ReactiveViewModel() : base()
        {
            
        }



        #region Region Navigation
        [Inject]
        public IRegionManager RegionManager { get; set; }



        protected void NavigateTo(string region, string uri)
        {

            RegionManager.Regions[region].RequestNavigate(new Uri(uri, UriKind.Relative));
        }

        #endregion



        #region IRegionMemberLifetime
        public virtual bool KeepAlive => true;

        #endregion
    }

}
