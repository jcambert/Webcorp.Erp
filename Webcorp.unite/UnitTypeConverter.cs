using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.unite
{
    internal sealed class UnitTypeConverter<T>:TypeConverter
        where T :IUnit<T>
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string) || sourceType == typeof(double)) return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var s = value as string;
            if (Nullable.GetUnderlyingType(typeof(T)) != null)
            {
                if (value == null || string.IsNullOrWhiteSpace(s))
                {
                    return null;
                }
            }

            if (s != null)
            {
                return (T)Activator.CreateInstance(typeof(T), s, null);
            }

            if (value is double)
            {
                var d = (double)value;
                return (T)Activator.CreateInstance(typeof(T), d.ToString(culture));
            }

            return default(T);
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string) || destinationType == typeof(double)) return true;
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            var q = value as IUnit<T>;
            if (q != null)
            {
                if (destinationType == typeof(string))
                {
                    return q.ToString(null, culture);
                }

                if (destinationType == typeof(double))
                {
                    return q.Value;
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
