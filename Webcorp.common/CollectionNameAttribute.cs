using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.common
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =false,Inherited =false)]
    public class CollectionNameAttribute:Attribute
    {
        public CollectionNameAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}
