using Ninject;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Model;

namespace Webcorp.rx_mvvm
{
    

    public class PropertyProviderFactory : IPropertyProviderFactory
    {
        private readonly IKernel _kernel;
        private readonly ISchedulers _schedulers;

        public PropertyProviderFactory(IKernel kernel, ISchedulers schedulers)
        {
            _schedulers = schedulers;
            _kernel = kernel;
        }

       /* public IPropertyProvider<T> Create<T>(T viewModelBase) where T : ViewModelBase
        {
            return new PropertyProvider<T>(viewModelBase, _schedulers);
        }

        public IPropertyProvider<T> Create<T>(ViewModelBase viewModelBase)
        {
            return new PropertyProvider<T>(viewModelBase, _schedulers);
        }*/

       /* public IPropertyProvider<T> Create<T>(T viewModelBase)
            where T : IEntityViewModel<IEntity>
        {
            
            IPropertyProvider<T> result = _kernel.Get<IPropertyProvider<T>>();
            //result.ViewModel = viewModelBase;
            return result;
        }*/

        public IPropertyProvider<T> Create<T>(T viewModelBase) where T : IViewModel// IEntityViewModel<E> where E : IEntity

        {
            IPropertyProvider<T> result = _kernel.Resolve<IPropertyProvider<T>>(viewModelBase);
            
            return result;
        }
    }
}
