using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.unite
{
    public interface IUnitProvider:IFormatProvider
    {
        CultureInfo Culture { get; }

        string Separator { get; set; }

        string Format<T>(string format, IFormatProvider formatProvider, T quantity) where T : IUnit<T>;

        IUnit GetUnit(string symbol);

        Dictionary<string, IUnit> GetUnits(Type type);

        void RegisterUnit(IUnit unit, string symbol);

        bool TrySetDisplayUnit(Type type, string symbol);

        IUnit GetDisplayUnit(Type type, out string symbol);

        string GetDisplayUnit(Type type);

        bool TryGetDisplayUnit(Type type, out IUnit unit, out string unitSymbol);

        bool TryParse(Type unitType, string input, IFormatProvider provider, out IUnit quantity);

        bool TryGetUnit(Type type, string name, out IUnit unit);

        bool TryGetUnit(string symbol, out IUnit unit);
    }
}
