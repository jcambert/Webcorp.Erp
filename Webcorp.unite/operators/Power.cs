using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.unite
{
    public partial class Power
    {
        /// <summary>
        ///     Implements the operator /.
        /// </summary>
        /// <param name="x"> The x. </param>
        /// <param name="y"> The y. </param>
        /// <returns> The result of the operator. </returns>
        public static ElectricVoltage operator /(Power x, ElectricCurrent y)
        {
            return new ElectricVoltage(x.Value / y.Value);
        }

        /// <summary>
        ///     Implements the operator /.
        /// </summary>
        /// <param name="x"> The x. </param>
        /// <param name="y"> The y. </param>
        /// <returns> The result of the operator. </returns>
        public static ElectricCurrent operator /(Power x, ElectricVoltage y)
        {
            return new ElectricCurrent(x.Value / y.Value);
        }

        /// <summary>
        ///     Implements the operator /.
        /// </summary>
        /// <param name="x"> The x. </param>
        /// <param name="y"> The y. </param>
        /// <returns> The result of the operator. </returns>
        public static Energy operator *(Power x, Time dt)
        {
            return new Energy(x.Value * dt.Value);
        }

    }
}
