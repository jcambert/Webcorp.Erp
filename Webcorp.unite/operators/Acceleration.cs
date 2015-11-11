using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.unite
{
    public partial class Acceleration
    {
        /// <summary>
        ///     Implements the operator *.
        /// </summary>
        /// <param name="x"> The x. </param>
        /// <param name="y"> The y. </param>
        /// <returns> The result of the operator. </returns>
        public static Velocity operator *(Acceleration x, Time y)
        {
            return new Velocity(x.Value * y.Value);
        }

        /// <summary>
        /// Implements the * operator for the product of <see cref="Acceleration" /> and <see cref="Length" />.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>The result of the operator.</returns>
        public static VelocitySquared operator *(Acceleration x, Length y)
        {
            return new VelocitySquared(x.Value * y.Value);
        }

        /// <summary>
        /// Implements the * operator for the product of <see cref="Acceleration" /> and <see cref="TimeSquared" />.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>The result of the operator.</returns>
        public static Length operator *(Acceleration x, TimeSquared y)
        {
            return new Length(x.Value * y.Value);
        }
    }
}
