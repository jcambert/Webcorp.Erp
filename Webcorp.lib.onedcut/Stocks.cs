using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.lib.onedcut
{
    public class Stocks:ReactiveList<BeamStock>
    {
        public Stocks():base()
        {

        }
        public Stocks(IEnumerable<BeamStock> e):base(e)
        {

        }
        public Stocks(IEnumerable<int> e)
        {
            foreach (var item in e)
            {
                Add(new BeamStock() { Length = item });
            }
        }
    }
}
