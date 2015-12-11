using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Controller;
using Webcorp.Model;

namespace Webcorp.Business
{
    [Business()]
    public abstract class AbstractBusiness<T>: Business<T>,IBusiness<T> where T : IEntity<string>
    {
    }
}
