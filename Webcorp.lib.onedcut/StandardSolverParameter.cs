using System;

namespace Webcorp.lib.onedcut
{
    internal class StandardSolverParameter : ISolverParameter
    {
        public double CrossoverProbability => 0.85;

        public int ElitePercentage => 2;

        public int InitialPopulationCount => 50;

        public int MaxEvaluation => 50;

        public double MutationProbability => 0.001;
    }
}