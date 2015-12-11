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
            try {
                return new Mass(m.Value * d);
            }
            catch
            {
                return new Mass(0);
            }
        }
        public static Currency operator *(MassLinear m, MassCurrency d)
        {
            try {
                return new Currency(m.Value * d.Value);
            }
            catch
            {
                return new Currency(0);
            }
        }
    }
}
