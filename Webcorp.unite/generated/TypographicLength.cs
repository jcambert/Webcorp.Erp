 
// --------------------------------------------------------------------------------------------------------------------
// <auto-generated> 
//   This code was generated by a T4 template. 
//
//   Changes to this file may cause incorrect behavior and will be lost if 
//   the code is regenerated. 
// </auto-generated>
// <copyright file="TypographicLength.cs" company="Webcorp">
//   Copyright (c) 2015 Webcorp contributors
// </copyright>
// <summary>
//   Represents the typographic length quantity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Webcorp.unite
{
    using System;
	using System.ComponentModel;
    using System.Globalization;
	using System.Collections.Generic;
	using System.Runtime.Serialization;
#if REACTIVE_CORE
	using ReactiveCore;
#endif
#if MONGO
	using MongoDB.Bson.Serialization.Attributes;
	using MongoDB.Bson.Serialization.Serializers;
    using MongoDB.Bson.Serialization;
#endif

    /// <summary>
    /// Represents the typographic length quantity.
    /// </summary>
    
#if !CORE
    [Serializable]
#endif
	[DataContract]
	[TypeConverter(typeof(UnitTypeConverter<TypographicLength>))]
    public partial class TypographicLength : Unit<TypographicLength>
    {
        /// <summary>
        /// The backing field for the <see cref="Centimetre" /> property.
        /// </summary>
		[Unit("cm", true)]
		private static readonly TypographicLength CentimetreField = new TypographicLength(0.01);

        /// <summary>
        /// The backing field for the <see cref="Point" /> property.
        /// </summary>
		[Unit("pt")]
		private static readonly TypographicLength PointField = new TypographicLength(0.0254 / 72);

        /// <summary>
        /// The backing field for the <see cref="Millimetre" /> property.
        /// </summary>
		[Unit("mm")]
		private static readonly TypographicLength MillimetreField = new TypographicLength(0.001);

        /// <summary>
        /// The backing field for the <see cref="Metre" /> property.
        /// </summary>
		[Unit("m")]
		private static readonly TypographicLength MetreField = new TypographicLength(1.0);

        /// <summary>
        /// The backing field for the <see cref="Inch" /> property.
        /// </summary>
		[Unit("in")]
		private static readonly TypographicLength InchField = new TypographicLength(0.0254);

		private readonly List<string> registeredSymbols;

		public override List<string> RegisteredSymbols=>registeredSymbols;
        /// <summary>
        /// The value.
        /// </summary>
        private double value;

		/// <summary>
        /// Initializes a new instance of the <see cref="TypographicLength"/> struct.
        /// </summary>
        public TypographicLength():this(0.0)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypographicLength"/> struct.
        /// </summary>
        /// <param name="value">
        /// The value. 
        /// </param>
        public TypographicLength(double value)
        {
            this.value = value;
			registeredSymbols = new List<string>() { "cm","pt","mm","m","in"};
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypographicLength"/> struct.
        /// </summary>
        /// <param name="value">
        /// The value. 
        /// </param>
        /// <param name="unitProvider">
        /// The unit provider. 
        /// </param>
        public TypographicLength(string value, IUnitProvider unitProvider = null)
        {
            this.value = Parse(value, unitProvider ?? UnitProvider.Default).value;
			registeredSymbols = new List<string>() { "cm","pt","mm","m","in"};
        }

        /// <summary>
        /// Gets the "cm" unit.
        /// </summary>
		[Unit("cm", true)]
		        public static TypographicLength Centimetre
        {
            get { return CentimetreField; }
        }

        /// <summary>
        /// Gets the "pt" unit.
        /// </summary>
		[Unit("pt")]
		        public static TypographicLength Point
        {
            get { return PointField; }
        }

        /// <summary>
        /// Gets the "mm" unit.
        /// </summary>
		[Unit("mm")]
		        public static TypographicLength Millimetre
        {
            get { return MillimetreField; }
        }

        /// <summary>
        /// Gets the "m" unit.
        /// </summary>
		[Unit("m")]
		        public static TypographicLength Metre
        {
            get { return MetreField; }
        }

        /// <summary>
        /// Gets the "in" unit.
        /// </summary>
		[Unit("in")]
		        public static TypographicLength Inch
        {
            get { return InchField; }
        }

        /// <summary>
        /// Gets or sets the typographic length as a string.
        /// </summary>
        /// <value>The string.</value>
        /// <remarks>
        /// This property is used for XML serialization.
        /// </remarks>
        //[XmlText]
        [DataMember]
		//[BsonSerializer(typeof(UnitSerializer))]
		//[BsonSerializer(typeof(TypographicLengthSerializer))]
        public string FValue
        {
            get
            {
                // Use round-trip format
                return this.ToString("R", CultureInfo.InvariantCulture);
            }

            set
            {
			#if REACTIVE_CORE
				this.RaiseAndSetIfChanged(ref this.value, Parse(value, CultureInfo.InvariantCulture).value);
			#else
                this.value = Parse(value, CultureInfo.InvariantCulture).value;
			#endif
            }
        }

        /// <summary>
        /// Gets or sets the value of the typographic length in the base unit.
        /// </summary>
        public override double Value
        {
            get{
                return this.value;
            }

			set{
				this.value = value;
			}

        }

		 /// <summary>
        /// Gets if typographic length is variable or not
        /// </summary>
		public override bool VariableValue { get {return false; } }

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
        /// A <see cref="TypographicLength"/> that represents the quantity in <paramref name="input" />. 
        /// </returns>
        public static TypographicLength Parse(string input, IFormatProvider provider, IUnitProvider unitProvider)
        {
            if (unitProvider == null)
            {
                unitProvider = provider as IUnitProvider ?? UnitProvider.Default;
            }

            TypographicLength value;
            if (!unitProvider.TryParse(input, provider, out value))
            {
				return new  TypographicLength(0);
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
        /// A <see cref="TypographicLength"/> that represents the quantity in <paramref name="input" />. 
        /// </returns>
        public static TypographicLength Parse(string input, IFormatProvider provider = null)
        {
            var unitProvider = provider as IUnitProvider ?? UnitProvider.Default;

            TypographicLength value;
            if (!unitProvider.TryParse(input, provider, out value))
            {
				return new  TypographicLength(0);
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
        /// A <see cref="TypographicLength"/> that represents the quantity in <paramref name="input" />. 
        /// </returns>
        public static TypographicLength Parse(string input, IUnitProvider unitProvider)
        {
            if (unitProvider == null)
            {
                unitProvider = UnitProvider.Default;
            }

            TypographicLength value;
            if (!unitProvider.TryParse(input, unitProvider.Culture, out value))
            {
				return new  TypographicLength(0);
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
        public static bool TryParse(string input, IFormatProvider provider, IUnitProvider unitProvider, out TypographicLength result)
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
        /// The <see cref="TypographicLength"/> .
        /// </returns>
        public static TypographicLength ParseJson(string input)
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
        public static TypographicLength operator +(TypographicLength x, TypographicLength y)
        {
            return new TypographicLength(x.value + y.value);
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
        public static TypographicLength operator /(TypographicLength x, double y)
        {
            return new TypographicLength(x.value / y);
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
        public static double operator /(TypographicLength x, TypographicLength y)
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
        public static bool operator ==(TypographicLength x, TypographicLength y)
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
        public static bool operator >(TypographicLength x, TypographicLength y)
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
        public static bool operator >=(TypographicLength x, TypographicLength y)
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
        public static bool operator !=(TypographicLength x, TypographicLength y)
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
        public static bool operator <(TypographicLength x, TypographicLength y)
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
        public static bool operator <=(TypographicLength x, TypographicLength y)
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
        public static TypographicLength operator *(double x, TypographicLength y)
        {
            return new TypographicLength(x * y.value);
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
        public static TypographicLength operator *(TypographicLength x, double y)
        {
            return new TypographicLength(x.value * y);
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
        public static TypographicLength operator -(TypographicLength x, TypographicLength y)
        {
            return new TypographicLength(x.value - y.value);
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
        public static TypographicLength operator +(TypographicLength x)
        {
            return new TypographicLength(x.value);
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
        public static TypographicLength operator -(TypographicLength x)
        {
            return new TypographicLength(-x.value);
        }

        /// <summary>
        /// Compares this instance to the specified <see cref="TypographicLength"/> and returns an indication of their relative values.
        /// </summary>
        /// <param name="other">
        /// The other <see cref="TypographicLength"/> . 
        /// </param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and the other value. 
        /// </returns>
        public override int CompareTo(TypographicLength other)
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
            return this.CompareTo((TypographicLength)obj);
        }

        /// <summary>
        /// Converts the quantity to the specified unit.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <returns>The amount of the specified unit.</returns>
		public override double ConvertTo(IUnit unit)
        {
            return this.ConvertTo((TypographicLength)unit);
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
        public double ConvertTo(TypographicLength unit)
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
            if (obj is TypographicLength)
            {
                return this.Equals((TypographicLength)obj);
            }

            return false;
        }

        /// <summary>
        /// Determines if the specified <see cref="TypographicLength"/> is equal to this instance.
        /// </summary>
        /// <param name="other">
        /// The other <see cref="TypographicLength"/> . 
        /// </param>
        /// <returns>
        /// True if the values are equal. 
        /// </returns>
        public override bool Equals(TypographicLength other)
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
            if (!(x is TypographicLength))
            {
                throw new InvalidOperationException("Can only add quantities of the same types.");
            }

            return new TypographicLength(this.value + x.Value);
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
#if MONGO
	public class TypographicLengthSerializer:SerializerBase<TypographicLength>{
		public override TypographicLength Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var up = UnitProvider.Default;
            IUnit result;
            if(up.TryGetUnit(typeof(TypographicLength), context.Reader.ReadString(), out result))
                return (TypographicLength)result;

            return base.Deserialize(context, args);
        } 
	}
#endif
	public enum TypographicLengthUnit{
		Centimetre,
		Point,
		Millimetre,
		Metre,
		Inch
	}

}
