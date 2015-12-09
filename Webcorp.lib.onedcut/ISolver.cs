using GAF;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Webcorp.lib.onedcut
{
    public interface ISolver
    {
        event EventHandler<SolverEventArgs> OnSolved;

        void Solve();

        Task SolveAsync();

        bool IsRunning { get; }

        void Halt();

        ReactiveList<Beam> Beams { get; set; }

        ReactiveList<BeamStock> Stocks { get; set; }

        int ElitePercentage { get; set; }

        double CrossoverProbability { get; set; }

        double MutationProbability { get; set; }

      
       
    
}
}