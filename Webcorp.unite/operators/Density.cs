using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.unite
{
    public partial class Density
    {
        /// <summary>
        ///     Implements the operator *.
        /// </summary>
        /// <param name="x"> The density. </param>
        /// <param name="y"> The volume. </param>
        /// <returns> The result of the operator. </returns>
        public static Mass operator *(Density x, Volume y)
        {
            return new Mass(x.Value * y.Value);
        }

        /// <summary>
        ///     Performs an implicit conversion from <see cref="Density" /> to <see cref="MassConcentration" />.
        /// </summary>
        /// <param name="d"> The density. </param>
        /// <returns> MassConcentration. </returns>
        public static implicit operator MassConcentration(Density d)
        {
            return new MassConcentration(d.Value);
        }

    }
}
