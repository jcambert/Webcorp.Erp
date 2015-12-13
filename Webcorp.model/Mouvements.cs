using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.reactive;

namespace Webcorp.Model
{
    public class Mouvements:ReactiveList<MouvementStock>
    {
        public Mouvements():base()
        {

        }
        public Mouvements(IList<MouvementStock> mvts):base(mvts)
        {
            
        }
    }
}
