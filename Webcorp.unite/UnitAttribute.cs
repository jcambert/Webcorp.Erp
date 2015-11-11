using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.unite
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class UnitAttribute:Attribute
    {
        private bool isDefault;
        private string symbol;

        public UnitAttribute(string symbol,bool isDefault= false)
        {
            this.symbol = symbol;
            this.isDefault = isDefault;
        }
        public string Symbol =>symbol;

        public bool isDefaultUnit => isDefault;

        public override object TypeId => new Guid(Symbol);
    }
}
