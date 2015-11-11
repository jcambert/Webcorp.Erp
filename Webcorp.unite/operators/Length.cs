using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.unite
{
    public partial class Length
    {
        /// <summary>
        ///     Implements the operator /.
        /// </summary>
        /// <param name="x"> The x. </param>
        /// <param name="y"> The y. </param>
        /// <returns> The result of the operator. </returns>
        public static Velocity operator /(Length x, Time y)
        {
            return new Velocity(x.Value / y.Value);
        }

        /// <summary>
        ///  Implements the operator /.
        /// </summary>
        /// <param name="x"> The x.</param>
        /// <param name="y"> The y.</param>
        /// <returns></returns>
        public static Time operator /(Length x,Velocity y)
        {
            return new Time(x.Value / y.Value);
        }

        /// <summary>
        ///     Implements the operator ^.
        /// </summary>
        /// <param name="x"> The x. </param>
        /// <param name="exp"> The exponent. </param>
        /// <returns> The result of the operator. </returns>
        public static IUnit operator ^(Length x, int exp)
        {
            if (exp == 2)
            {
                return new Area(x.Value * x.Value);
            }

            if (exp == 3)
            {
                return new Volume(x.Value * x.Value * x.Value);
            }

            throw new NotSupportedException();
        }

        /// <summary>
        ///     Implements the operator *.
        /// </summary>
        /// <param name="x"> The x. </param>
        /// <param name="y"> The y. </param>
        /// <returns> The result of the operator. </returns>
        public static Area operator *(Length x, Length y)
        {
            return new Area(x.Value * y.Value);
        }

        /// <summary>
        ///     Implements the operator *.
        /// </summary>
        /// <param name="x"> The x. </param>
        /// <param name="y"> The y. </param>
        /// <returns> The result of the operator. </returns>
        public static Volume operator *(Length x, Area y)
        {
            return new Volume(x.Value * y.Value);
        }

        /// <summary>
        ///     Implements the operator *.
        /// </summary>
        /// <param name="x"> The x. </param>
        /// <param name="y"> The y. </param>
        /// <returns> The result of the operator. </returns>
        public static double operator *(Length x, TypographicResolution y)
        {
            return x.Value * y.Value;
        }

        /// <summary>
        /// Implements the / operator for the product of <see cref="Acceleration" /> and <see cref="TimeSquared" />.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>The result of the operator.</returns>
        public static Acceleration operator /(Length x, TimeSquared y)
        {
            return new Acceleration(x.Value / y.Value);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="TypographicLength"/> to <see cref="Length"/>.
        /// </summary>
        /// <param name="x">The quantity to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Length(TypographicLength x)
        {
            return x.ConvertTo(TypographicLength.Centimetre) * Centimetre;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Length"/> to <see cref="TypographicLength"/>.
        /// </summary>
        /// <param name="x">The quantity to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator TypographicLength(Length x)
        {
            return x.ConvertTo(Centimetre) * TypographicLength.Centimetre;
        }

        /// <summary>
        ///     Cubes this length.
        /// </summary>
        /// <returns> The volume. </returns>
        public Volume Cubed()
        {
            return new Volume(this.Value * this.Value * this.Value);
        }

        /// <summary>
        ///     Squares this length.
        /// </summary>
        /// <returns> The area. </returns>
        public Area Squared()
        {
            return new Area(this.Value * this.Value);
        }
    }
}
