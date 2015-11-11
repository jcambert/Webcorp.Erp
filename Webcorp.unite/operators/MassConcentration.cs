using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.unite
{
    public partial class MassConcentration
    {
        /// <summary>
        ///     Implements the operator *.
        /// </summary>
        /// <param name="x"> The concentration. </param>
        /// <param name="y"> The volume. </param>
        /// <returns> The result of the operator. </returns>
        public static Mass operator *(MassConcentration x, Volume y)
        {
            return new Mass(x.Value * y.Value);
        }
    }
}
