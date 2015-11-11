using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Webcorp.common;

public static class ReflectionExtensions
{
    public static T Create<T>(this Type t,  Type[] types,params object[] parameters)
    {
        if (typeof(T) != t)  throw new ArgumentException("T and type t must be same");
        types.ThrowIfNull<ArgumentNullException>("types cannot be null");
        parameters = parameters ?? new object[] { };
        parameters.ThrowIfNull<ArgumentNullException>("parameters cannot be null");
        (types.Length != parameters.Length).ThrowIf<ArgumentException>("types and parameters must have same length");
        return Create<T>(types);
    }

    private static T Create<T>( Type[] types, params object[] parameters)
    {
        try
        {
            var ctor = typeof(T).GetConstructor(types);
            return (T)ctor.Invoke(new object[] { });
        }
        catch (Exception)
        {

            throw;
        }
    }


    public static List<PropertyInfo> GetPropertiesSortedByFieldOrder<T>(this Type t)where T :OrderedAttribute
    {
        PropertyInfo[] allProperties = t
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => p.GetCustomAttribute<T>() != null)
            .Select(x => new
            {
                Property = x,
                Attribute = (T)Attribute.GetCustomAttribute(x, typeof(T), true)
            })
            .OrderBy(x => x.Attribute != null ? x.Attribute.Order : -1)
            .Select(x => x.Property)
            .ToArray();
        return allProperties.ToList();
    }
}

