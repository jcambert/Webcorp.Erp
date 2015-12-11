using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.Controller
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class BusinessAttribute:Attribute
    {
        public BusinessAttribute(int order=0)
        {
            this.Order = order;
        }

        public int Order { get; private set; }
    }
}
