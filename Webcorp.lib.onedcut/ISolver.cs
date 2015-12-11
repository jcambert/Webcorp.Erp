using GAF;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Webcorp.Model;

namespace Webcorp.lib.onedcut
{
    public interface ISolver
    {
        event EventHandler<SolverEventArgs> OnSolved;

        void Solve();

        Task SolveAsync();

        bool IsRunning { get; }

        void Halt();

        ReactiveList<BeamToCut> Beams { get; set; }

        ReactiveList<BeamStock> Stocks { get; set; }

        Article Beam { get; set; }

        int ElitePercentage { get; set; }

        double CrossoverProbability { get; set; }

        double MutationProbability { get; set; }

      
       
    
}
}