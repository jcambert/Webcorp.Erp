using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Webcorp.Model;

namespace Webcorp.rx_mvvm
{

    public interface IPropertyProvider<T>  //where T : IViewModel
    {
        IPropertySubject<K> CreateProperty<K>(Expression<Func<T, K>> expression);
        IPropertySubject<K> CreateProperty<K>(Expression<Func<T, K>> expression, K value);
        IPropertySubject<K> CreateProperty<K>(Expression<Func<T, K>> expression, IObservable<K> values);

        ICommandObserver<K> CreateCommand<K>();
        ICommandObserver<K> CreateCommand<K>( bool isEnabled);
        ICommandObserver<K> CreateCommand<K>(IObservable<bool> isEnabled);
    }
    public interface IPropertyProvider<T,E> : IPropertyProvider<T>  where T : IViewModel
    {
      
    }

    

    
}
 