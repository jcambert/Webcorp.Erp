using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


public static class ExceptionExtensions
{
    /// <summary>
    /// Throw an Exception
    /// </summary>
    /// <param name="e"></param>
    public static void Throw(this Exception e)
    {
        if (e != null) throw e;
    }

    /// <summary>
    /// Throw Exception
    /// if e is null then throw Exception with message
    /// </summary>
    /// <param name="e"></param>
    /// <param name="message"></param>
    public static void Throw(this Exception e, string message)
    {
        e.Throw<Exception>(message);
    }
    /// <summary>
    /// Throw Exception
    /// if e is null then throw T Exception with message
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="e"></param>
    /// <param name="message"></param>
    public static void Throw<T>(this Exception e,string message) where T :Exception
    {
        if (e != null)
            throw e;
        
        message.ThrowIfNullOrEmpty("message cannot be null nor empty");

        throw typeof(T).Create<T>(new Type[] {typeof(string) },message);
       
    }

    public static void ThrowIfNull<T>(this object o,string message) where T :Exception
    {
        message.ThrowIfNullOrEmpty("message cannot be null nor empty");
        if (o == null)
            typeof(T).Create<T>(new Type[] { typeof(string) }, message).Throw();
    }

    public static void ThrowIf<T>(this bool b, string message) where T : Exception
    {
        message.ThrowIfNullOrEmpty("message cannot be null nor empty");
        if (b) typeof(T).Create<T>(new Type[] { typeof(string) }, message).Throw();
    }

    public static void ThrowIf<T>(this Expression<Func<bool>> e,string message)where T : Exception
    {
        message.ThrowIfNullOrEmpty("message cannot be null nor empty");
        if(e.Compile().Invoke()) typeof(T).Create<T>(new Type[] { typeof(string) }, message).Throw();
    }
}

