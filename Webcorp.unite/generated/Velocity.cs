 
// --------------------------------------------------------------------------------------------------------------------
// <auto-generated> 
//   This code was generated by a T4 template. 
//
//   Changes to this file may cause incorrect behavior and will be lost if 
//   the code is regenerated. 
// </auto-generated>
// <copyright file="Velocity.cs" company="Webcorp">
//   Copyright (c) 2015 Webcorp contributors
// </copyright>
// <summary>
//   Represents the velocity quantity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Webcorp.unite
{
    using System;
	using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;
	using System.Collections.Generic;
	using MongoDB.Bson.Serialization.Attributes;
	using MongoDB.Bson.Serialization.Serializers;
    using MongoDB.Bson.Serialization;

    /// <summary>
    /// Represents the velocity quantity.
    /// </summary>
    [DataContract]
#if !PCL
    [Serializable]
    [TypeConverter(typeof(UnitTypeConverter<Velocity>))]
#endif
    public partial class Velocity : Unit<Velocity>
    {
        /// <summary>
        /// The backing field for the <see cref="MetrePerSecond" /> property.
        /// </summary>
		[Unit("m/s", true)]
		private static readonly Velocity MetrePerSecondField = new Velocity(1);

        /// <summary>
        /// The backing field for the <see cref="MetrePerMinute" /> property.
        /// </summary>
		[Unit("m/m")]
		private static readonly Velocity MetrePerMinuteField = new Velocity(0.016666);

        /// <summary>
        /// The backing field for the <see cref="MillimetrePerMinute" /> property.
        /// </summary>
		[Unit("mm/m")]
		private static readonly Velocity MillimetrePerMinuteField = new Velocity(0.0000166666);

        /// <summary>
        /// The backing field for the <see cref="KilometrePerHour" /> property.
        /// </summary>
		[Unit("km/h")]
		private static readonly Velocity KilometrePerHourField = new Velocity(1/3.6);

        /// <summary>
        /// The backing field for the <see cref="Knot" /> property.
        /// </summary>
		[Unit("knot")]
		private static readonly Velocity KnotField = new Velocity(0.514444444444444);

        /// <summary>
        /// The backing field for the <see cref="FootPerSecond" /> property.
        /// </summary>
		[Unit("ft/s")]
		private static readonly Velocity FootPerSecondField = new Velocity(0.3048);

        /// <summary>
        /// The backing field for the <see cref="MilePerHour" /> property.
        /// </summary>
		[Unit("mph")]
		private static readonly Velocity MilePerHourField = new Velocity(0.44704);

		private readonly List<string> registeredSymbols;

		public override List<string> RegisteredSymbols=>registeredSymbols;
        /// <summary>
        /// The value.
        /// </summary>
        private double value;

		/// <summary>
        /// Initializes a new instance of the <see cref="Velocity"/> struct.
        /// </summary>
        public Velocity():this(0.0)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Velocity"/> struct.
        /// </summary>
        /// <param name="value">
        /// The value. 
        /// </param>
        public Velocity(double value)
        {
            this.value = value;
			registeredSymbols = new List<string>() { "m/s","m/m","mm/m","km/h","knot","ft/s","mph"};
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Velocity"/> struct.
        /// </summary>
        /// <param name="value">
        /// The value. 
        /// </param>
        /// <param name="unitProvider">
        /// The unit provider. 
        /// </param>
        public Velocity(string value, IUnitProvider unitProvider = null)
        {
            this.value = Parse(value, unitProvider ?? UnitProvider.Default).value;
			registeredSymbols = new List<string>() { "m/s","m/m","mm/m","km/h","knot","ft/s","mph"};
        }

        /// <summary>
        /// Gets the "m/s" unit.
        /// </summary>
		[Unit("m/s", true)]
		        public static Velocity MetrePerSecond
        {
            get { return MetrePerSecondField; }
        }

        /// <summary>
        /// Gets the "m/m" unit.
        /// </summary>
		[Unit("m/m")]
		        public static Velocity MetrePerMinute
        {
            get { return MetrePerMinuteField; }
        }

        /// <summary>
        /// Gets the "mm/m" unit.
        /// </summary>
		[Unit("mm/m")]
		        public static Velocity MillimetrePerMinute
        {
            get { return MillimetrePerMinuteField; }
        }

        /// <summary>
        /// Gets the "km/h" unit.
        /// </summary>
		[Unit("km/h")]
		        public static Velocity KilometrePerHour
        {
            get { return KilometrePerHourField; }
        }

        /// <summary>
        /// Gets the "knot" unit.
        /// </summary>
		[Unit("knot")]
		        public static Velocity Knot
        {
            get { return KnotField; }
        }

        /// <summary>
        /// Gets the "ft/s" unit.
        /// </summary>
		[Unit("ft/s")]
		        public static Velocity FootPerSecond
        {
            get { return FootPerSecondField; }
        }

        /// <summary>
        /// Gets the "mph" unit.
        /// </summary>
		[Unit("mph")]
		        public static Velocity MilePerHour
        {
            get { return MilePerHourField; }
        }

        /// <summary>
        /// Gets or sets the velocity as a string.
        /// </summary>
        /// <value>The string.</value>
        /// <remarks>
        /// This property is used for XML serialization.
        /// </remarks>
        //[XmlText]
        [DataMember]
		//[BsonSerializer(typeof(UnitSerializer))]
		//[BsonSerializer(typeof(VelocitySerializer))]
        public string FValue
        {
            get
            {
                // Use round-trip format
                return this.ToString("R", CultureInfo.InvariantCulture);
            }

            set
            {
                this.value = Parse(value, CultureInfo.InvariantCulture).value;
            }
        }

        /// <summary>
        /// Gets the value of the velocity in the base unit.
        /// </summary>
        public override double Value
        {
            get
            {
                return this.value;
            }
        }

        /// <summary>
        /// Converts a string representation of a quantity in a specific culture-specific format with a specific unit provider.
        /// </summary>
        /// <param name="input">
        /// A string that contains the quantity to convert. 
        /// </param>
        /// <param name="provider">
        /// An object that supplies culture-specific formatting information about <paramref name="input" />. If not specified, the culture of the default <see cref="UnitProvider" /> is used. 
        /// </param>
        /// <param name="unitProvider">
        /// The unit provider. If not specified, the default <see cref="UnitProvider" /> is used.
        /// </param>
        /// <returns>
        /// A <see cref="Velocity"/> that represents the quantity in <paramref name="input" />. 
        /// </returns>
        public static Velocity Parse(string input, IFormatProvider provider, IUnitProvider unitProvider)
        {
            if (unitProvider == null)
            {
                unitProvider = provider as IUnitProvider ?? UnitProvider.Default;
            }

            Velocity value;
            if (!unitProvider.TryParse(input, provider, out value))
            {
				return new  Velocity(0);
                //throw new FormatException("Invalid format.");
            }

            return value;
        }

        /// <summary>
        /// Converts a string representation of a quantity in a specific culture-specific format.
        /// </summary>
        /// <param name="input">
        /// A string that contains the quantity to convert. 
        /// </param>
        /// <param name="provider">
        /// An object that supplies culture-specific formatting information about <paramref name="input" />. If not specified, the culture of the default <see cref="UnitProvider" /> is used. 
        /// </param>
        /// <returns>
        /// A <see cref="Velocity"/> that represents the quantity in <paramref name="input" />. 
        /// </returns>
        public static Velocity Parse(string input, IFormatProvider provider = null)
        {
            var unitProvider = provider as IUnitProvider ?? UnitProvider.Default;

            Velocity value;
            if (!unitProvider.TryParse(input, provider, out value))
            {
				return new  Velocity(0);
                //throw new FormatException("Invalid format.");
            }

            return value;
        }

        /// <summary>
        /// Converts a string representation of a quantity with a specific unit provider.
        /// </summary>
        /// <param name="input">
        /// A string that contains the quantity to convert. 
        /// </param>
        /// <param name="unitProvider">
        /// The unit provider. If not specified, the default <see cref="UnitProvider" /> is used.
        /// </param>
        /// <returns>
        /// A <see cref="Velocity"/> that represents the quantity in <paramref name="input" />. 
        /// </returns>
        public static Velocity Parse(string input, IUnitProvider unitProvider)
        {
            if (unitProvider == null)
            {
                unitProvider = UnitProvider.Default;
            }

            Velocity value;
            if (!unitProvider.TryParse(input, unitProvider.Culture, out value))
            {
				return new  Velocity(0);
                //throw new FormatException("Invalid format.");
            }

            return value;
        }

        /// <summary>
        /// Tries to parse the specified string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="provider">The format provider.</param>
        /// <param name="unitProvider">The unit provider.</param>
        /// <param name="result">The result.</param>
        /// <returns><c>true</c> if the string was parsed, <c>false</c> otherwise.</returns>
        public static bool TryParse(string input, IFormatProvider provider, IUnitProvider unitProvider, out Velocity result)
        {
            if (unitProvider == null)
            {
                unitProvider = provider as IUnitProvider ?? UnitProvider.Default;
            }

            return unitProvider.TryParse(input, provider, out result);
        }

        /// <summary>
        /// Parses the specified JSON string.
        /// </summary>
        /// <param name="input">The JSON input.</param>
        /// <returns>
        /// The <see cref="Velocity"/> .
        /// </returns>
        public static Velocity ParseJson(string input)
        {
            return Parse(input, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="x">
        /// The first value. 
        /// </param>
        /// <param name="y">
        /// The second value. 
        /// </param>
        /// <returns>
        /// The result of the operator. 
        /// </returns>
        public static Velocity operator +(Velocity x, Velocity y)
        {
            return new Velocity(x.value + y.value);
        }

        /// <summary>
        /// Implements the operator /.
        /// </summary>
        /// <param name="x">
        /// The x. 
        /// </param>
        /// <param name="y">
        /// The y. 
        /// </param>
        /// <returns>
        /// The result of the operator. 
        /// </returns>
        public static Velocity operator /(Velocity x, double y)
        {
            return new Velocity(x.value / y);
        }

        /// <summary>
        /// Implements the operator /.
        /// </summary>
        /// <param name="x">
        /// The x. 
        /// </param>
        /// <param name="y">
        /// The y. 
        /// </param>
        /// <returns>
        /// The result of the operator. 
        /// </returns>
        public static double operator /(Velocity x, Velocity y)
        {
            return x.value / y.value;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="x">
        /// The x. 
        /// </param>
        /// <param name="y">
        /// The y. 
        /// </param>
        /// <returns>
        /// The result of the operator. 
        /// </returns>
        public static bool operator ==(Velocity x, Velocity y)
        {
            return x.value.Equals(y.value);
        }

        /// <summary>
        /// Implements the operator &gt;.
        /// </summary>
        /// <param name="x">
        /// The x. 
        /// </param>
        /// <param name="y">
        /// The y. 
        /// </param>
        /// <returns>
        /// The result of the operator. 
        /// </returns>
        public static bool operator >(Velocity x, Velocity y)
        {
            return x.value > y.value;
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="x">
        /// The x. 
        /// </param>
        /// <param name="y">
        /// The y. 
        /// </param>
        /// <returns>
        /// The result of the operator. 
        /// </returns>
        public static bool operator >=(Velocity x, Velocity y)
        {
            return x.value >= y.value;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="x">
        /// The x. 
        /// </param>
        /// <param name="y">
        /// The y. 
        /// </param>
        /// <returns>
        /// The result of the operator. 
        /// </returns>
        public static bool operator !=(Velocity x, Velocity y)
        {
            return !x.value.Equals(y.value);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="x">
        /// The x. 
        /// </param>
        /// <param name="y">
        /// The y. 
        /// </param>
        /// <returns>
        /// The result of the operator. 
        /// </returns>
        public static bool operator <(Velocity x, Velocity y)
        {
            return x.value < y.value;
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="x">
        /// The x. 
        /// </param>
        /// <param name="y">
        /// The y. 
        /// </param>
        /// <returns>
        /// The result of the operator. 
        /// </returns>
        public static bool operator <=(Velocity x, Velocity y)
        {
            return x.value <= y.value;
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="x">
        /// The x. 
        /// </param>
        /// <param name="y">
        /// The y. 
        /// </param>
        /// <returns>
        /// The result of the operator. 
        /// </returns>
        public static Velocity operator *(double x, Velocity y)
        {
            return new Velocity(x * y.value);
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="x">
        /// The x. 
        /// </param>
        /// <param name="y">
        /// The y. 
        /// </param>
        /// <returns>
        /// The result of the operator. 
        /// </returns>
        public static Velocity operator *(Velocity x, double y)
        {
            return new Velocity(x.value * y);
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="x">
        /// The x. 
        /// </param>
        /// <param name="y">
        /// The y. 
        /// </param>
        /// <returns>
        /// The result of the operator. 
        /// </returns>
        public static Velocity operator -(Velocity x, Velocity y)
        {
            return new Velocity(x.value - y.value);
        }

        /// <summary>
        /// Implements the unary plus operator.
        /// </summary>
        /// <param name="x">
        /// The x. 
        /// </param>
        /// <returns>
        /// The result of the operator. 
        /// </returns>
        public static Velocity operator +(Velocity x)
        {
            return new Velocity(x.value);
        }

        /// <summary>
        /// Implements the unary minus operator.
        /// </summary>
        /// <param name="x">
        /// The x. 
        /// </param>
        /// <returns>
        /// The result of the operator. 
        /// </returns>
        public static Velocity operator -(Velocity x)
        {
            return new Velocity(-x.value);
        }

        /// <summary>
        /// Compares this instance to the specified <see cref="Velocity"/> and returns an indication of their relative values.
        /// </summary>
        /// <param name="other">
        /// The other <see cref="Velocity"/> . 
        /// </param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and the other value. 
        /// </returns>
        public override int CompareTo(Velocity other)
        {
            return this.value.CompareTo(other.value);
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the 
        /// current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings: 
        /// Value Meaning Less than zero This instance is less than <paramref name="obj" />. Zero This instance is equal to 
        /// <paramref name="obj" />. Greater than zero This instance is greater than <paramref name="obj" />.
        /// </returns>
        public override int CompareTo(object obj)
        {
            return this.CompareTo((Velocity)obj);
        }

        /// <summary>
        /// Converts the quantity to the specified unit.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <returns>The amount of the specified unit.</returns>
		public override double ConvertTo(IUnit unit)
        {
            return this.ConvertTo((Velocity)unit);
        }

        /// <summary>
        /// Converts to the specified unit.
        /// </summary>
        /// <param name="unit">
        /// The unit. 
        /// </param>
        /// <returns>
        /// The value in the specified unit. 
        /// </returns>
        public double ConvertTo(Velocity unit)
        {
            return this.value / unit.Value;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="System.Object"/> to compare with this instance. 
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c> . 
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is Velocity)
            {
                return this.Equals((Velocity)obj);
            }

            return false;
        }

        /// <summary>
        /// Determines if the specified <see cref="Velocity"/> is equal to this instance.
        /// </summary>
        /// <param name="other">
        /// The other <see cref="Velocity"/> . 
        /// </param>
        /// <returns>
        /// True if the values are equal. 
        /// </returns>
        public override bool Equals(Velocity other)
        {
            return this.value.Equals(other.value);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        /// <summary>
        /// Multiplies by the specified number.
        /// </summary>
        /// <param name="x">The number.</param>
        /// <returns>The new quantity.</returns>
        public override IUnit MultiplyBy(double x)
        {
            return this * x;
        }

        /// <summary>
        /// Adds the specified quantity.
        /// </summary>
        /// <param name="x">The quantity to add.</param>
        /// <returns>The sum.</returns>
        public override IUnit Add(IUnit x)
        {
            if (!(x is Velocity))
            {
                throw new InvalidOperationException("Can only add quantities of the same types.");
            }

            return new Velocity(this.value + x.Value);
        }

        /// <summary>
        /// Sets the value from the specified string.
        /// </summary>
        /// <param name="s">
        /// The s. 
        /// </param>
        /// <param name="provider">
        /// The provider. 
        /// </param>
        public void SetFromString(string s, IUnitProvider provider)
        {
            this.value = Parse(s, provider).value;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance. 
        /// </returns>
        public override string ToString()
        {
            return this.ToString(null, UnitProvider.Default);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <param name="formatProvider">
        /// The format provider. 
        /// </param>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance. 
        /// </returns>
        public string ToString(IFormatProvider formatProvider)
        {
            var unitProvider = formatProvider as IUnitProvider ?? UnitProvider.Default;

            return this.ToString(null, formatProvider, unitProvider);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <param name="format">
        /// The format. 
        /// </param>
        /// <param name="formatProvider">
        /// The format provider. 
        /// </param>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance. 
        /// </returns>
        public override string ToString(string format, IFormatProvider formatProvider = null)
        {
            var unitProvider = formatProvider as IUnitProvider ?? UnitProvider.Default;

            return this.ToString(format, formatProvider, unitProvider);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <param name="format">
        /// The format. 
        /// </param>
        /// <param name="formatProvider">
        /// The format provider. 
        /// </param>
        /// <param name="unitProvider">
        /// The unit provider. 
        /// </param>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance. 
        /// </returns>
        public  string ToString(string format, IFormatProvider formatProvider, IUnitProvider unitProvider)
        {
            if (unitProvider == null)
            {
                unitProvider = formatProvider as IUnitProvider ?? UnitProvider.Default;
            }

            return unitProvider.Format(format, formatProvider, this);
        }
    }

	public class VelocitySerializer:SerializerBase<Velocity>{
		public override Velocity Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var up = UnitProvider.Default;
            IUnit result;
            if(up.TryGetUnit(typeof(Velocity), context.Reader.ReadString(), out result))
                return (Velocity)result;

            return base.Deserialize(context, args);
        } 
	}
}
