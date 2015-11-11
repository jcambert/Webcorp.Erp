using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.unite
{
    public partial class MassCurrency
    {

        /// <summary>
        ///     Implements the operator *.
        /// </summary>
        /// <param name="x"> The concentration. </param>
        /// <param name="y"> The volume. </param>
        /// <returns> The result of the operator. </returns>
        public static Currency operator *(MassCurrency y,Mass x )
        {
            return new Currency(x.Value * y.Value);
        }
    }
}
