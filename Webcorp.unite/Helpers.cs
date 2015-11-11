using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.unite
{
    public class Helpers
    {
        public static TauxHorraire GetThFromAppConfig(string key,int defaultValue)
        {
            TauxHorraire result;
            try
            {
                string th = ConfigurationManager.AppSettings[key];
                if (TauxHorraire.TryParse(th, UnitProvider.Default, UnitProvider.Default, out result))
                    return result;
                int res0;
                if (Int32.TryParse(th, out res0))
                    return res0 * TauxHorraire.EuroHeure;
                return defaultValue * TauxHorraire.EuroHeure;
            }
            catch
            {
                return defaultValue * TauxHorraire.EuroHeure;
            }
        }

        public static Currency GetCurrencyFromAppConfig(string key, int defaultValue)
        {
            Currency result;
            try
            {
                string th = ConfigurationManager.AppSettings[key];
                if (Currency.TryParse(th, UnitProvider.Default, UnitProvider.Default, out result))
                    return result;
                int res0;
                if (Int32.TryParse(th, out res0))
                    return res0 * Currency.Euro;
                return defaultValue * Currency.Euro;
            }
            catch
            {
                return defaultValue * Currency.Euro;
            }
        }

        public static Time GetTimeFromAppConfig(string key, int defaultValue)
        {
            Time result;
            try
            {
                string th = ConfigurationManager.AppSettings[key];
                if (Time.TryParse(th, UnitProvider.Default, UnitProvider.Default, out result))
                    return result;
                int res0;
                if (Int32.TryParse(th, out res0))
                    return res0 * Time.Minute;
                return defaultValue * Time.Minute;
            }
            catch
            {
                return defaultValue * Time.Minute;
            }
        }

        public static double GetDoubleFromAppConfig(string key, double defaultValue)
        {
            double result;
            try
            {
                string th = ConfigurationManager.AppSettings[key];
                if (Double.TryParse(th, out result))
                    return result;
                
                return defaultValue;
            }
            catch
            {
                return defaultValue ;
            }
        }
    }
}
