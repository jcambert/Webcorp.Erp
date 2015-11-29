using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;


public static class ObjectExtension
{
    public static bool IsNull(this object o) => o == null;

    public static bool IsNotNull(this object o) => !o.IsNull();
    
    public static T IfIsNull<T>(this T o, Func<T> f) => o.IsNull() ? f() : o;

   public static bool IsPositive(this int i)
    {
        return i >= 0;
    }

    public static bool IsBound(this int i,int min,int max)=> i >= min && i <= max;


    public static bool IsNotBound(this int i, int min, int max) => i<min || i>max;

    public static T Clone<T>(this T source)
    {
        MemoryStream stream1 = new MemoryStream();
        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
        ser.WriteObject(stream1, source);

        stream1.Position = 0;
        T result = (T)ser.ReadObject(stream1);

        return result;
    }
}

