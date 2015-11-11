using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.unite;

namespace Webcorp.Model.Quotation
{
    public class CalculTranche<T,U> : ICalculTranche<T,U> where T : Unit<T> where U:Unit<U>
    {
        List<Tranche<T,U>> list=new List<Tranche<T, U>>();

        public bool Complexe { get; private set; }

        public U Montant { get; private set; }
        public Currency MontantForfait { get; private set; }
        public bool Simple { get; private set; }

        public void Add(U montant, Currency montantForfait)
        {
            Complexe.ThrowIf<ArgumentException>("You cannot add simple tranche when working on complexe tranche");
            Simple = true;
            this.Montant = montant;
            this.MontantForfait = montantForfait;
        }

        public void Add( T maxi, U montant,bool forfait=false)
        {
            Complexe = true;
            Simple.ThrowIf<ArgumentException>("You cannot add a tranche when working on simple tranche");
            list.Add(new Tranche<T, U>() {  Maxi = maxi, Montant = montant, Forfait = forfait });
        }

        public Tranche<T,U> Find(T unite)
        {

            Tranche<T, U> result = null;
            foreach (var item in list)
            {
                if (item.Maxi > unite && result != null && result?.Maxi < unite)
                    result = item;
                else if (result == null)
                    result = item;
            }

            return result ?? FindForfait() ;
        }

        public Tranche<T,U> FindForfait()=> list.Where(t => t.Forfait).FirstOrDefault();
        
    }

    public class Tranche<T,U>: IComparable<Tranche<T, U>>,IComparer<Tranche<T, U>> where T : Unit<T> where U :Unit<U>
    {
        

        public T Maxi { get; set; }

        public bool Forfait { get; set; }

        public U Montant { get; set; }

        public Currency MontantForfait { get; set; }

        [BsonIgnore]
        public bool Simple => MontantForfait != null;

        [BsonIgnore ]
        public bool Complex => !Simple;



        public int CompareTo(Tranche<T, U> other)
        {
            return Maxi.CompareTo(other.Maxi);
        }

        public int Compare(Tranche<T, U> x, Tranche<T, U> y)
        {
            var res= y.Maxi.Value - x.Maxi.Value;
            if (res == 0) return 0;
            return res < 0 ? -1 : 1;
        }
    }
}
