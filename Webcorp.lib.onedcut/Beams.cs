using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.lib.onedcut
{
    public class Beams : ReactiveList<Beam>
    {
        public Beams():base()
        {

        }

        public Beams(IEnumerable<Beam> e):base(e)
        {

        }
    }
}
