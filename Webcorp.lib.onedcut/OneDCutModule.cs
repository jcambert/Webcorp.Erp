using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.lib.onedcut
{
    public class OneDCutModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISolverParameter>().To<StandardSolverParameter>();
            Bind<ISolver>().To<Solver>();
        }
    }
}
