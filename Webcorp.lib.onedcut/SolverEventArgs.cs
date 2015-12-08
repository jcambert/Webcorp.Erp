using GAF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.lib.onedcut
{
    public class SolverEventArgs:GaEventArgs
    {
        public SolverEventArgs(Population population,int generation,long evaluations):base(population,generation,evaluations)
        {
            
        }

        public Chromosome Fittest => Population.GetTop(1)[0];
    }
}
