using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.common
{
    public interface IShouldDisposable
    {
        void ShouldDispose(IDisposable disposable);
    }
}
