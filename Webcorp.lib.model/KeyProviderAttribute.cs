using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.common;

namespace Webcorp.Model
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class KeyProviderAttribute : OrderedAttribute
    {
        private int order;

        public KeyProviderAttribute(int order=0)
        {
            this.order = order;
        }
    }
}
