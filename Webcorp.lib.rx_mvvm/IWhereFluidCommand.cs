using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.rx_mvvm
{
    public interface IWhereFluidCommand<T>
    {
        void Where(IObservable<bool> observer);
    }
}
