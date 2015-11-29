using Ninject;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Webcorp.reactive;
using ReactiveUI;
using Webcorp.Model;
namespace Webcorp.rx_mvvm
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
