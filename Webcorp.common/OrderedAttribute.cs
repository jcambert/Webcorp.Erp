using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.common
{
    public class OrderedAttribute:Attribute
    {
        public OrderedAttribute(int order=0)
        {
            this.Order = order;
        }

        public int Order { get; private set; }
    }
}
