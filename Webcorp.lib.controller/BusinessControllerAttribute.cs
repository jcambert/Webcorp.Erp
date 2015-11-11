using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.Controller
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =false,Inherited =true)]
    public sealed class BusinessControllerAttribute:Attribute
    {
        public BusinessControllerAttribute(/*Type t,*/int order=0)
        {
            //if (!Type.IsAssignableFrom(typeof(BusinessController<>))) throw new ArgumentException("Type must be a child of BusinessController<>");
            //this.Type = t;
            this.Order = order;
        }

        public int Order { get; private set; }
       // public Type Type { get; private set; }
    }
}
