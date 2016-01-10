using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


public static class ConfigurationManagerExtensions
{
    public static void GetValue<TValue>(this Configuration config, string key, ref TValue prop, TValue @default)
    {
        string result;
        try
        {
            result = config.AppSettings.Settings[key].Value;
            if (typeof(TValue) == typeof(double))
            {
                double d;
                if (double.TryParse(result, out d))
                {
                    prop = (TValue)(object)d;
                }
            }
            else if (typeof(TValue) == typeof(int))
            {
                int d;
                if (Int32.TryParse(result, out d))
                {
                    prop = (TValue)(object)d;
                }
            }
            else if (typeof(TValue) == typeof(string))
            {

                prop = (TValue)(object)result;

            }
        }
        catch
        {
            prop = @default;

        }
    }

    public static void SetValue<TValue>(this Configuration config, TValue value, [CallerMemberName]string property = "")
    {
        if (property.IsNullOrEmpty()) throw new ArgumentException("Property cannot be null nor empty");
        try
        {
            config.AppSettings.Settings[property].Value = value.ToString();
        }
        catch
        {
            config.AppSettings.Settings.Add(property, value.ToString());
        }
        
    }
}

