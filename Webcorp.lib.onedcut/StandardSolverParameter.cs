using System;

namespace Webcorp.lib.onedcut
{
    internal class StandardSolverParameter : ISolverParameter
    {
        public double CrossoverProbability => 0.85;

        public int ElitePercentage => 2;

        public int InitialPopulationCount => 100;

        public int MaxEvaluation => 200;

        public double MutationProbability => 0.001;
    }
}