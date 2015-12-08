using GAF;
using ReactiveUI;
using System;
using System.Collections.Generic;

namespace Webcorp.lib.onedcut
{
    public interface ISolver
    {
        event EventHandler<SolverEventArgs> OnSolved;

        void Solve();

        void SolveAsync();

        bool IsRunning { get; }

        void Halt();

        ReactiveList<Beam> Beams { get; set; }

        ReactiveList<BeamStock> Stocks { get; set; }

        int ElitePercentage { get; set; }

        double CrossoverProbability { get; set; }

        double MutationProbability { get; set; }

      
       
    
}
}