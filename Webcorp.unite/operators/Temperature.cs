using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.unite
{
    public partial class Temperature
    {
        /// <summary>
        ///     Implements the operator +.
        /// </summary>
        /// <param name="x"> The x. </param>
        /// <param name="y"> The y. </param>
        /// <returns> The result of the operator. </returns>
        public static Temperature operator +(Temperature x, TemperatureDifference y)
        {
            return new Temperature(x.value + y.Value);
        }

        /// <summary>
        ///     Implements the operator -.
        /// </summary>
        /// <param name="x"> The x. </param>
        /// <param name="y"> The y. </param>
        /// <returns> The result of the operator. </returns>
        /*public static TemperatureDifference operator -(Temperature x, Temperature y)
        {
            return new TemperatureDifference(x.value - y.value);
        }*/
    }
}
