using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string s) => s == null || s.Trim().Length == 0;

    public static void ThrowIfNullOrEmpty(this string s,string message=null)
    {
        if (s.IsNullOrEmpty()) throw new ArgumentException(message??"String cannot be null or empty");
    }

    public static string FormatWith(this string s,params object[] args)
    {
        return string.Format(s, args);
    }

    public static string Add(this string s,string arg)
    {
        return s + arg;
    }

    public static string Left(this string s,int len)
    {
        if (s.IsNullOrEmpty()) return string.Empty;
        if (s.Trim().Length <= len) return s;

        return s.Substring(0, len);
    }
}

