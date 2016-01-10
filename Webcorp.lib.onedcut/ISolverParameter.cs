namespace Webcorp.lib.onedcut
{
    public interface ISolverParameter
    {
        int ElitePercentage { get; set; }
        double CrossoverProbability { get; set; }
        double MutationProbability { get; set; }
        int MaxEvaluation { get; set; }
        int InitialPopulationCount { get; set; }

        void Save();
    }
}