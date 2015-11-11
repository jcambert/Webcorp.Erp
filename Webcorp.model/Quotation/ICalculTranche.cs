using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.unite;

namespace Webcorp.Model.Quotation
{
    public interface ICalculTranche<T, U> where T : Unit<T> where U : Unit<U>
    {
        void Add(U montant, Currency montantForfait);

        void Add(T maxi, U montant, bool forfait = false);

        Tranche<T, U> Find(T unite);

        Tranche<T, U> FindForfait();

        U Montant { get; }
        Currency MontantForfait { get; }
        bool Simple { get; }

    }
}
