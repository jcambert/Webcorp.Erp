using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.reactive
{
    public interface IViewModelStatus<T> where T : class
    {


        string Status { get; set; }
        ReactiveVMCollection<T> ViewModel { get; set; }

        void Register(string v, Action<ReactiveVMCollection<T>> p);
    }
}
