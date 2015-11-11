using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.unite;

namespace Webcorp.Model.Quotation
{
    public interface ICalculPrestation<T,U> where T :Unit<T> where U:Unit<U>
    {
        Currency Calculate( T unite, ICalculTranche<T,U> tranches);
    }
}
