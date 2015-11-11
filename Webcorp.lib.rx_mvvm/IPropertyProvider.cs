using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Webcorp.Model;

namespace Webcorp.rx_mvvm
{
    public interface IPropertyProvider<T,E> : IDisposable where T : IEntityViewModel<E> where E : IEntity
    {
        T ViewModel { get;  }

        IPropertySubject<K> CreateProperty<K>(Expression<Func<T, K>> expression);
        IPropertySubject<K> CreateProperty<K>(Expression<Func<T, K>> expression, K value);
        IPropertySubject<K> CreateProperty<K>(Expression<Func<T, K>> expression, IObservable<K> values);

        ICommandObserver<K> CreateCommand<K>(Expression<Func<T, ICommand>> expression);
        ICommandObserver<K> CreateCommand<K>(Expression<Func<T, ICommand>> expression, bool isEnabled);
        ICommandObserver<K> CreateCommand<K>(Expression<Func<T, ICommand>> expression, IObservable<bool> isEnabled);
    }
}
 