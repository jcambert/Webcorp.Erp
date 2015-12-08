namespace Webcorp.lib.onedcut
{
    public interface ISolverParameter
    {
        int ElitePercentage { get; }
        double CrossoverProbability { get; }
        double MutationProbability { get; }
        int MaxEvaluation { get; }
        int InitialPopulationCount { get; }
    }
}