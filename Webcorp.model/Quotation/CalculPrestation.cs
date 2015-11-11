using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.unite;

namespace Webcorp.Model.Quotation
{
    

    public class CalculPrestation<T,U> : ICalculPrestation<T, U> where T : Unit<T> where U :   Unit<U>
    {
        public Currency Calculate(T unite, ICalculTranche<T, U> tranches)
        {
            try
            {
                if (tranches.Simple)
                {
                    var result = new Currency(tranches.Montant.Value * unite.Value);
                    return result < tranches.MontantForfait ? tranches.MontantForfait : result;
                }
                else
                {
                    Tranche<T, U> tranche = tranches.Find(unite);
                    Currency result = tranche.Forfait ? new Currency(tranche.Montant.Value) : new Currency(tranche.Montant.Value * unite.Value);
                    return result;
                }
            }
            catch (Exception)
            {
                return -1 * Currency.Euro;
            }
        }

       
    }
}
