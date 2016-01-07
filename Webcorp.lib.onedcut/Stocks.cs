using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.lib.onedcut
{
    public class Stocks : ReactiveList<BeamStock>
    {
        public Stocks() : base()
        {

        }
        public Stocks(IEnumerable<BeamStock> e) : base(e)
        {

        }
        public Stocks(IEnumerable<int> e, IEnumerable<int> qty)
        {
            if (e.Count() != qty.Count()) throw new ArgumentException("Each of length and quantity must have same item number");
            var lengths = e.ToArray();
            var qties = qty.ToArray();
            for (int j = 0; j < qties.Count(); j++)
            {
                for (int i = 0; i < qties[j]; i++)
                {
                    this.Add(new BeamStock() { Length = lengths[j] });
                }
            }

        }
        public Stocks(IEnumerable<int> e)
        {
            foreach (var item in e)
            {
                Add(new BeamStock() { Length = item });
            }
        }

        public Stocks(int length,int qty)
        {
            for (int i = 0; i < qty; i++)
            {
                Add(new BeamStock() { Length = length });
            }
        }
        public int Length
        {
            get
            {
                var i = 0;
                for (int j = 0; j < this.Count; j++)
                {
                    i += this[j].Length;
                }
                return i;
            }
        }

    }
}
