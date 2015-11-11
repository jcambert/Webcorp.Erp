using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.unite
{
    public partial class ElectricResistance
    {
        /// <summary>
        ///     Implements the operator *.
        /// </summary>
        /// <param name="x"> The x. </param>
        /// <param name="y"> The y. </param>
        /// <returns> The result of the operator. </returns>
        public static ElectricVoltage operator *(ElectricResistance x, ElectricCurrent y)
        {
            return new ElectricVoltage(x.Value * y.Value);
        }
    }
}
