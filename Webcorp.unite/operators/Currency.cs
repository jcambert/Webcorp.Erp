using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.unite
{
    public partial class Currency : IUnit<Currency>
    {
        public void SetChange(double value)
        {
            this.value = value;
        }

        public static Currency operator +(Currency x, double y)
        {
            return new Currency(x.value + y);
        }
    }
}
