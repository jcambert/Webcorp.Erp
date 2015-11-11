using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.unite
{
    public partial class Capacitance
    {
        /// <summary>
        ///     Implements the operator *.
        /// </summary>
        /// <param name="x"> The x. </param>
        /// <param name="y"> The y. </param>
        /// <returns> The result of the operator. </returns>
        public static ElectricCharge operator *(Capacitance x, ElectricVoltage y)
        {
            return new ElectricCharge(x.Value * y.Value);
        }



    }
}
