using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Controller;
using Webcorp.Model;

namespace Webcorp.Business
{
   [BusinessController()]
    public abstract class AbstractBusinessController<T>: BusinessController<T> where T :IEntity<string>
    {
    }
}
