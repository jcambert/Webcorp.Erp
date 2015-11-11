using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.unite
{
    public partial class Torque
    {
        /// <summary>
        ///     Performs an implicit conversion from <see cref="Energy" /> to <see cref="Torque" />.
        /// </summary>
        /// <param name="m"> The m. </param>
        /// <returns> The result of the conversion. </returns>
        public static implicit operator Torque(Energy m)
        {
            return new Torque(m.Value);
        }

        /// <summary>
        ///     Implements the operator *.
        /// </summary>
        /// <param name="x"> The torque. </param>
        /// <param name="y"> The angle. </param>
        /// <returns> The result of the operator. </returns>
        public static Energy operator *(Torque x, Angle y)
        {
            return new Energy(x.Value * y.Value);
        }

        /// <summary>
        /// Implements torque divided by length.
        /// </summary>
        /// <param name="x">The torque.</param>
        /// <param name="y">The length.</param>
        /// <returns>The resulting force.</returns>
        public static Force operator /(Torque x, Length y)
        {
            return new Force(x.Value / y.Value);
        }
    }
}
