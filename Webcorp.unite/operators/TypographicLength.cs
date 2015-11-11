﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.unite
{
    public partial class TypographicLength
    {
        /// <summary>
        /// Divides a number of dots by a length to get the resolution.
        /// </summary>
        /// <param name="x"> The number of dots. </param>
        /// <param name="y"> The length. </param>
        /// <returns> The resolution. </returns>
        public static TypographicResolution operator /(double x, TypographicLength y)
        {
            return new TypographicResolution(x / y.Value);
        }

        /// <summary>
        /// Multiplies a <see cref="TypographicLength" /> with a <see cref="TypographicResolution" /> to get the number of dots.
        /// </summary>
        /// <param name="x"> The length. </param>
        /// <param name="y"> The resolution. </param>
        /// <returns> The number of dots. </returns>
        public static double operator *(TypographicLength x, TypographicResolution y)
        {
            return x.value * y.Value;
        }
    }
}
