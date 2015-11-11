using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Webcorp.rx_mvvm
{
    public interface ICommandObserver<out T> : ICommand
    {
        IObservable<T> OnExecute { get; }
        IObserver<bool> SetCanExecute { get; }
    }

}
