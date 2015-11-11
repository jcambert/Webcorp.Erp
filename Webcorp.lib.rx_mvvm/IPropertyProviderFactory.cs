using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Model;

namespace Webcorp.rx_mvvm
{
    /*public interface IPropertyProviderFactory<T> where T : IEntityViewModel<IEntity>
    {
        IPropertyProvider<T> Create(T viewModel);
    }*/
    public interface IPropertyProviderFactory
    {

        IPropertyProvider<T,E> Create<T,E>(T viewModelBase) where T : IEntityViewModel<E> where E : IEntity;
        
    }
}
