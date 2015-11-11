using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.unite
{
    public partial class VolumetricWaterContent
    {
        /// <summary>
        ///     Performs an implicit conversion from <see cref="VolumetricWaterContent" /> to <see cref="Fraction" />.
        /// </summary>
        /// <param name="c"> The volumetric water content. </param>
        /// <returns> Fraction. </returns>
        public static implicit operator Fraction(VolumetricWaterContent c)
        {
            return new Fraction(c.value);
        }

    }
}
