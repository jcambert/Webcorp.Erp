using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.rx_mvvm
{
    public interface IViewModelStatus<T> where T : class
    {


        string Status { get; set; }
        ReactiveViewModel<T> ViewModel { get; set; }

        void Register(string v, Action<ReactiveViewModel<T>> p);
    }
}
