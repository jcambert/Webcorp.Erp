using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.rx_mvvm
{
   public interface IMessage<T>
    {
         Action<T> Action { get;  }

         string Info { get; }
    }
}
