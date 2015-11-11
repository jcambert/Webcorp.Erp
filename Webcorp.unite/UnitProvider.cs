using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.unite
{
    public class UnitProvider : IUnitProvider
    {

        private readonly Dictionary<Type, UnitDefinition> displayUnits = new Dictionary<Type, UnitDefinition>();

        private readonly Dictionary<Type, Dictionary<string, IUnit>> units = new Dictionary<Type, Dictionary<string, IUnit>>();

        private static readonly IUnitProvider defaultup;

        private readonly CultureInfo culture;

        static UnitProvider()
        {
            defaultup = new UnitProvider(typeof(Length).Assembly);
        }

        public static IUnitProvider Default => defaultup;

        public UnitProvider(Assembly a, CultureInfo culture = null)
            : this(culture)
        {
            this.RegisterUnits(a);
        }

        public UnitProvider(CultureInfo culture = null)
        {
            this.Separator = " ";
            this.culture = culture ?? CultureInfo.CurrentCulture;
        }

        public CultureInfo Culture => culture;

        public string Separator { get; set; }

        public string Format<T>(string format, IFormatProvider provider, T quantity) where T : IUnit<T>
        {
            if (double.IsNaN(quantity.Value))
            {
                return double.NaN.ToString(provider);
            }

            if (double.IsPositiveInfinity(quantity.Value))
            {
                return double.PositiveInfinity.ToString(provider);
            }

            if (double.IsNegativeInfinity(quantity.Value))
            {
                return double.NegativeInfinity.ToString(provider);
            }

            var unit = default(string);
            if (!string.IsNullOrEmpty(format))
            {
                var unitStart = format.IndexOf('[');
                if (unitStart >= 0)
                {
                    var unitEnd = format.IndexOf(']', unitStart + 1);
                    if (unitEnd < 0)
                    {
                        throw new FormatException("Unmatched [ in format string.");
                    }

                    unit = format.Substring(unitStart + 1, unitEnd - unitStart - 1);
                    format = format.Remove(unitStart, unitEnd - unitStart + 1).Trim();
                }
            }

            // unit=null: convert to display unit, show display unit
            // unit=empty: convert to display unit, but do not show
            // otherwise: convert to specified unit, show specified unit
            var showUnit = unit != string.Empty;

            // find the conversion unit
            T q;
            if (!string.IsNullOrEmpty(unit))
            {
                q = this.GetUnit<T>(unit);
            }
            else
            {
                if (!this.TryGetDisplayUnit(out q, out unit))
                {
                    return null;
                }
            }

            // Convert the value to a string
            string s = quantity.ConvertTo(q).ToString(format, provider ?? this);

            if (!showUnit)
            {
                // Return the value only
                return s;
            }

            // Temperatures should have a space before the unit
            // Angles should not have a space before ° symbol
            var separator = this.Separator;
            // ReSharper disable once CSharpWarnings::CS0184
            var isTemperature = quantity is Temperature;
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (!isTemperature && (string.IsNullOrEmpty(unit) || unit.StartsWith("°")))
            {
                separator = string.Empty;
            }

            return string.Concat(s, separator, unit).Trim();
        }

        public string GetDisplayUnit(Type type)
        {
            UnitDefinition ud;
            if (!this.displayUnits.TryGetValue(type, out ud))
            {
                throw new InvalidOperationException("No display unit defined for " + type);
            }

            return ud.Name;
        }

        public IUnit GetDisplayUnit(Type type, out string symbol)
        {
            UnitDefinition ud;
            if (!this.displayUnits.TryGetValue(type, out ud))
            {
                throw new InvalidOperationException("No display unit defined for " + type);
            }

            symbol = ud.Name;
            return ud.Unit;
        }

        public object GetFormat(Type formatType)
        {
            return this.Culture.GetFormat(formatType);
        }

        public IUnit GetUnit(string symbol)
        {
            foreach (var u in this.units.Values)
            {
                IUnit v;
                if (u.TryGetValue(symbol, out v)) return v;
            }

            return null;
        }

        public Dictionary<string, IUnit> GetUnits(Type type)
        {
            return this.units[type];
        }

        public void RegisterUnit(IUnit unit, string symbol)
        {
            var type = unit.GetType();
            if (!this.units.ContainsKey(type))
            {
                this.units.Add(type, new Dictionary<string, IUnit>());
            }

            if (this.units[type].ContainsKey(symbol))
            {
                throw new InvalidOperationException(string.Format("{0} is already added to {1}", symbol, type));
            }

            this.units[type].Add(symbol, unit);
        }

        public bool TryGetDisplayUnit(Type type, out IUnit unit, out string unitSymbol)
        {
            UnitDefinition ud;
            if (!this.displayUnits.TryGetValue(type, out ud))
            {
                unit = null;
                unitSymbol = null;
                return false;
            }

            unitSymbol = ud.Name;
            unit = ud.Unit;
            return true;
        }

        public bool TryParse(Type unitType, string input, IFormatProvider provider, out IUnit quantity)
        {
            unitType = Nullable.GetUnderlyingType(unitType) ?? unitType;

            if (string.IsNullOrEmpty(input))
            {
                quantity = (IUnit)Activator.CreateInstance(unitType);
                return true;
            }

            if (string.Equals(input, double.NaN.ToString(provider)))
            {
                quantity = (IUnit)Activator.CreateInstance(unitType, double.NaN);
                return true;
            }

            if (string.Equals(input, double.PositiveInfinity.ToString(provider)))
            {
                quantity = (IUnit)Activator.CreateInstance(unitType, double.PositiveInfinity);
                return true;
            }

            if (string.Equals(input, double.NegativeInfinity.ToString(provider)))
            {
                quantity = (IUnit)Activator.CreateInstance(unitType, double.NegativeInfinity);
                return true;
            }

            string unitString;
            double value;
            if (!input.TrySplit(provider, out value, out unitString))
            {
                quantity = null;
                return false;
            }

            IUnit unit;
            if (string.IsNullOrEmpty(unitString))
            {
                // No unit was provided - use the display unit
                string name;
                if (!this.TryGetDisplayUnit(unitType, out unit, out name))
                {
                    quantity = null;
                    return false;
                }
            }
            else
            {
                // Find the unit
                if (!this.TryGetUnit(unitType, unitString, out unit))
                {
                    quantity = null;
                    return false;
                }
            }

            quantity = unit.MultiplyBy(value);
            return true;
        }

        public bool TrySetDisplayUnit(Type type, string symbol)
        {
            IUnit unit;
            if (!this.TryGetUnit(type, symbol, out unit))
            {
                return false;
            }

            this.displayUnits[type] = new UnitDefinition { Name = symbol, Unit = unit };
            return true;
        }

        private T GetUnit<T>(string name)
        {
            Dictionary<string, IUnit> typeUnits;
            if (!this.units.TryGetValue(typeof(T), out typeUnits))
            {
                throw new InvalidOperationException(string.Format("No units registered for {0}.", typeof(T)));
            }

            IUnit unit;
            if (!typeUnits.TryGetValue(name, out unit))
            {
                if (string.IsNullOrEmpty(name))
                {
                    T displayUnit;
                    string displayUnitName;
                    if (this.TryGetDisplayUnit(out displayUnit, out displayUnitName))
                    {
                        return displayUnit;
                    }
                }

                throw new FormatException(string.Format("Unit '{0}' not found in type {1}.", name, typeof(T)));
            }

            return (T)unit;
        }

        public bool TryGetUnit(Type type, string name, out IUnit unit)
        {
            Dictionary<string, IUnit> d;
            if (!this.units.TryGetValue(type, out d))
            {
                unit = default(IUnit);
                return false;
            }

            IUnit u;
            if (name == null || !d.TryGetValue(name, out u))
            {
                unit = default(IUnit);
                return false;
            }

            unit = u;
            return true;
        }

        public bool TryGetUnit(string value, out IUnit unit)
        {
            var s = value.Split(' ');
            var _value = s[0];
            var _symbol = s[1];
            foreach(var du in displayUnits.Values)
            {
                if (du.Unit.RegisteredSymbols.Contains(_symbol))
                {
                   unit= (IUnit)Activator.CreateInstance(du.GetType(), double.Parse(_value));
                    return true;
                }

            }
            unit = null;
            return false;
        }

        private struct UnitDefinition
        {
            /// <summary>
            ///   Gets or sets the name.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            ///   Gets or sets the unit.
            /// </summary>
            public IUnit Unit { get; set; }
        }
    }
}
