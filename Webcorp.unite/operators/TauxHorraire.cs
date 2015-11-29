using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.unite
{
    public partial class TauxHorraire
    {
        public static Currency operator *(TauxHorraire x, Time y)
        {
            try {
                return new Currency(x.Value * y.ConvertTo(Time.Hour));
            }
            catch
            {
                return 0 * Currency.Euro;
            }
        }


    }
}
