namespace Webcorp.lib.onedcut
{
    public interface ISolverParameter
    {
        int ElitePercentage { get; set; }
        double CrossoverProbability { get; set; }
        double MutationProbability { get; set; }
        int MaxEvaluation { get; set; }
        int InitialPopulationCount { get; set; }
        int CuttingWidth { get; set; }
        int MiniLength { get; set; }
        void Save();
    }
}