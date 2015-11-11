using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.unite
{
    public partial class ElectricConductance
    {
        /// <summary>
        ///     Implements the operator /.
        /// </summary>
        /// <param name="c"> The conductance. </param>
        /// <param name="l"> The length. </param>
        /// <returns> The result of the operator. </returns>
        public static ElectricConductivity operator /(ElectricConductance c, Length l)
        {
            return new ElectricConductivity(c.Value / l.Value);
        }

    }
}
