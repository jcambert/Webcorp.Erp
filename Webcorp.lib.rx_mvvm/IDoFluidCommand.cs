using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.rx_mvvm
{
    public interface IDoFluidCommand<T> : IDisposable
    {
        IWhereFluidCommand<T> Do(Action<T> action);
    }
}
