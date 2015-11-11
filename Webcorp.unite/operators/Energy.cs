using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.unite
{
    public partial class Energy
    {
        /// <summary>
        ///     Implements the operator /.
        /// </summary>
        /// <param name="x"> The x. </param>
        /// <param name="y"> The y. </param>
        /// <returns> The result of the operator. </returns>
        public static HeatCapacity operator /(Energy x, TemperatureDifference y)
        {
            return new HeatCapacity(x.value / y.Value);
        }

        /// <summary>
        ///     Performs an implicit conversion from <see cref="Torque" /> to <see cref="Energy" />.
        /// </summary>
        /// <param name="m"> The m. </param>
        /// <returns> The result of the conversion. </returns>
        public static implicit operator Energy(Torque m)
        {
            return new Energy(m.Value);
        }

        /// <summary>
        ///     Implements the operator /.
        /// </summary>
        /// <param name="x"> The x. </param>
        /// <param name="y"> The y. </param>
        /// <returns> The result of the operator. </returns>
        public static Power operator /(Energy x, Time dt)
        {
            return new Power(x.Value / dt.Value);
        }

    }
}
