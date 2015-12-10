using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.lib.onedcut
{
    public class Beams : ReactiveList<BeamToCut>
    {
        public Beams():base()
        {

        }

        public Beams(IEnumerable<BeamToCut> e):base(e)
        {

        }
    }
}
