﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.unite
{
    public partial class Area
    {
        /// <summary>
        /// Calculates the square root of the specified area.
        /// </summary>
        /// <param name="l">
        /// The area. 
        /// </param>
        /// <returns>
        /// The length. 
        /// </returns>
        public static Length Sqrt(Area l)
        {
            return new Length(System.Math.Sqrt(l.value));
        }

        /// <summary>
        ///     Implements the operator /.
        /// </summary>
        /// <param name="a1"> The area. </param>
        /// <param name="a2"> The area. </param>
        /// <returns> The result of the operator. </returns>
        public static Length operator /(Area a1, Length a2)
        {
            return new Length(a1.value / a2.Value);
        }

        /// <summary>
        ///     Implements the operator *.
        /// </summary>
        /// <param name="a1"> The area. </param>
        /// <param name="a2"> The area. </param>
        /// <returns> The result of the operator. </returns>
        public static Volume operator *(Area a1, Length a2)
        {
            return new Volume(a1.value * a2.Value);
        }

        /// <summary>
        ///     Implements the operator *.
        /// </summary>
        /// <param name="a1"> The area. </param>
        /// <param name="a2"> The area. </param>
        /// <returns> The result of the operator. </returns>
        public static SecondMomentOfArea operator *(Area a1, Area a2)
        {
            return new SecondMomentOfArea(a1.value * a2.Value);
        }
    }
}
