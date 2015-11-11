﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.unite
{
    public partial class Mass
    {
        /// <summary>
        ///     Implements the operator /.
        /// </summary>
        /// <param name="m"> The mass. </param>
        /// <param name="v"> The volume. </param>
        /// <returns> The result of the operator. </returns>
        public static Density operator /(Mass m, Volume v)
        {
            return new Density(m.Value / v.Value);
        }

        /// <summary>
        ///     Implements the operator /.
        /// </summary>
        /// <param name="m"> The mass. </param>
        /// <param name="d"> The density. </param>
        /// <returns> The result of the operator. </returns>
        public static Volume operator /(Mass m, Density d)
        {
            return new Volume(m.Value / d.Value);
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="m">The mass.</param>
        /// <param name="a">The area.</param>
        /// <returns>The mass moment of inertia.</returns>
        public static MassMomentOfInertia operator *(Mass m, Area a)
        {
            return new MassMomentOfInertia(m.Value * a.Value);
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="m">The mass.</param>
        /// <param name="a">The acceleration.</param>
        /// <returns>The force.</returns>
        public static Force operator *(Mass m, Acceleration a)
        {
            return new Force(m.Value * a.Value);
        }

        /// <summary>
        /// Implements the operator * for the product of <see cref="Mass" /> and <see cref="VelocitySquared" />.
        /// </summary>
        /// <param name="m">The mass.</param>
        /// <param name="v2">The velocity squared.</param>
        /// <returns>The energy.</returns>
        public static Energy operator *(Mass m, VelocitySquared v2)
        {
            return new Energy(m.Value * v2.Value);
        }


        /// <summary>
        ///     Implements the operator *.
        /// </summary>
        /// <param name="x"> The concentration. </param>
        /// <param name="y"> The volume. </param>
        /// <returns> The result of the operator. </returns>
        public static MassCurrency operator *(Mass x, Currency y)
        {
            return new MassCurrency(x.Value * y.Value);
        }

        /// <summary>
        ///     Implements the operator *.
        /// </summary>
        /// <param name="x"> The concentration. </param>
        /// <param name="y"> The volume. </param>
        /// <returns> The result of the operator. </returns>
        public static Currency operator *(Mass x, MassCurrency y)
        {
            return new Currency(x.Value * y.Value);
        }
    }
}
