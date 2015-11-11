using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class ObjectExtension
{
    public static bool IsNull(this object o) => o == null;

    public static bool IsNotNull(this object o) => !o.IsNull();
    
    public static T IfIsNull<T>(this T o, Func<T> f) => o.IsNull() ? f() : o;

   /* public static void ThrowIfIsNull<E>(this object o,string message) where E :Exception
    {
        if (o.IsNotNull()) return;
        throw (E)Activator.CreateInstance(typeof(E), message);
    }*/
}

