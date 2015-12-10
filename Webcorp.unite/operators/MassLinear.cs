using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.unite
{
    public partial class MassLinear
    {
        public static Mass operator *(MassLinear m, int d)
        {
            return new Mass(m.Value *d);
        }
        public static Currency operator *(MassLinear m, MassCurrency d)
        {
            return new Currency(m.Value * d.Value);
        }
    }
}
