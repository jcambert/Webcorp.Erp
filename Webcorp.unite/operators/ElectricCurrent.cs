using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.unite
{
    public partial class ElectricCurrent
    {
        /// <summary>
        ///     Implements the operator *.
        /// </summary>
        /// <param name="x"> The x. </param>
        /// <param name="y"> The y. </param>
        /// <returns> The result of the operator. </returns>
        public static ElectricCharge operator *(ElectricCurrent x, Time y)
        {
            return new ElectricCharge(x.Value * y.Value);
        }

        /// <summary>
        ///     Implements the operator *.
        /// </summary>
        /// <param name="x"> The x. </param>
        /// <param name="y"> The y. </param>
        /// <returns> The result of the operator. </returns>
        public static Power operator *(ElectricCurrent x, ElectricVoltage y)
        {
            return new Power(x.Value * y.Value);
        }

        /// <summary>
        ///     Implements the operator *.
        /// </summary>
        /// <param name="x"> The x. </param>
        /// <param name="y"> The y. </param>
        /// <returns> The result of the operator. </returns>
        public static ElectricVoltage operator *(ElectricCurrent x, ElectricResistance y)
        {
            return new ElectricVoltage(x.Value * y.Value);
        }
    }
}
