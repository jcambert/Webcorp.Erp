using GAF;
using GAF.Operators;
using GAF.Extensions;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Model;
using ReactiveUI;
namespace Webcorp.lib.onedcut
{
    public class Solver : CustomReactiveObject, ISolver, IInitializable
    {
        public event EventHandler<SolverEventArgs> OnSolved = delegate { };

        private GeneticAlgorithm _ga;
        private Elite eliteOperator;
        private Crossover crossoverOperator;
        private SwapMutate mutateOperator;
        private int cuttingWidth;
        private bool hasChanged, beamsChanged;
        private ReactiveList<BeamToCut> beams;
        private ReactiveList<BeamStock> stocks;
        private Article beam;
        private Population initialPopulation;
        private FitnessFunction _fitnessfunction;
        public Solver()
        {
            ShouldDispose(this.Changed.Subscribe(_ => { hasChanged = true; beamsChanged = (_.PropertyName == "Beams" || _.PropertyName == "Stocks" || _.PropertyName == "Beam"); }));

        }

        public void SaveParameters()
        {
            SolverParameter.Save();
        }

        public int ElitePercentage { get { return eliteOperator.Percentage; } set { eliteOperator.Percentage = value; this.RaisePropertyChanged(); } }

        public double CrossoverProbability { get { return crossoverOperator.CrossoverProbability; } set { crossoverOperator.CrossoverProbability = value; this.RaisePropertyChanged(); } }

        public double MutationProbability { get { return mutateOperator.MutationProbability; } set { mutateOperator.MutationProbability = value; this.RaisePropertyChanged(); } }

        public int CuttingWidth { get { return cuttingWidth; } set { this.RaiseAndSetIfChanged(ref cuttingWidth, value); } }

        public ReactiveList<BeamToCut> Beams { get { return beams; } set { this.RaiseAndSetIfChanged(ref beams, value); } }

        public ReactiveList<BeamStock> Stocks { get { return stocks; } set { this.RaiseAndSetIfChanged(ref stocks, value); } }

        public Article Beam { get { return beam; } set { this.RaiseAndSetIfChanged(ref beam, value); } }


        int _initialPopulationCount;
        public int InitialPopulationCount { get { return _initialPopulationCount; } set { this.RaiseAndSetIfChanged(ref _initialPopulationCount, value); } }

        int _maxEvaluation;
        public int MaxEvaluation { get { return _maxEvaluation; } set { this.RaiseAndSetIfChanged(ref _maxEvaluation, value); } }
        [Inject]
        public ISolverParameter SolverParameter { get; set; }

        public FitnessFunction FitnessFunction { get { return _fitnessfunction; } set { this.RaiseAndSetIfChanged(ref _fitnessfunction, value); } }



        protected double CalculateFitness(Chromosome solution)
        {
            int totWaste = 0, totStock = 0;
            foreach (var gene in solution.Genes)
            {
                totWaste += (gene.ObjectValue as CutPlan).Waste;
                totStock += (gene.ObjectValue as CutPlan).StockLength;
            }
            return (totWaste / totStock)*100;
        }


        public void Solve()
        {

            if (beamsChanged)
            {
                initialPopulation = CreateInitialePopulation(InitialPopulationCount);
                beamsChanged = false;
            }
            if (hasChanged) internalInit();
            _ga.Run(SolverParameter.MaxEvaluation);
        }

        public async Task SolveAsync()
        {


            if (beamsChanged)
            {
                initialPopulation = await CreateInitialePopulationAsync(InitialPopulationCount);
                beamsChanged = false;
            }
            if (hasChanged) internalInit();
            _ga.RunAsync(SolverParameter.MaxEvaluation);
        }

        public void Initialize()
        {
            _maxEvaluation = SolverParameter.MaxEvaluation;
            _initialPopulationCount = SolverParameter.InitialPopulationCount;
            eliteOperator = new Elite(SolverParameter.ElitePercentage);
            crossoverOperator = new Crossover(SolverParameter.CrossoverProbability) { CrossoverType = CrossoverType.DoublePointOrdered };
            mutateOperator = new SwapMutate(SolverParameter.MutationProbability);
            internalInit();

        }

        private void internalInit()
        {
#if DEBUG
            if (hasChanged)
                Debug.WriteLine("a property has changed. We recreate a genetic algorithm");
#endif
            if (_ga != null)
            {
                _ga.OnRunComplete -= Ga_OnRunComplete;
                _ga = null;
            }
            _ga = new GeneticAlgorithm(initialPopulation, FitnessFunction ?? CalculateFitness);
            _ga.OnRunComplete += Ga_OnRunComplete;
            _ga.Operators.Add(eliteOperator);
            _ga.Operators.Add(crossoverOperator);
            _ga.Operators.Add(mutateOperator);
            hasChanged = false;
        }

        private void Ga_OnRunComplete(object sender, GaEventArgs e)
        {
#if DEBUG
            Debug.WriteLine("Run Complete ...");
#endif
            var args = new SolverEventArgs(e.Population, e.Generation, e.Evaluations, Beams);

            OnSolved(this, args);
        }

        public bool IsRunning => _ga.IsRunning;



        public void Halt() { _ga.Halt(); }


        private async Task<Population> CreateInitialePopulationAsync(int totPop)
        {
            return await new TaskFactory<Population>().StartNew(() => { return CreateInitialePopulation(totPop); });
        }

        private Population CreateInitialePopulation(int totPop)
        {
            if (Stocks == null) throw new ArgumentNullException("You must set Stocks");
            if (Beams == null) throw new ArgumentNullException("You must set Beams");
            if (totPop <= 0) throw new ArgumentException("Initial Total population count must be more than 0");
            if (Stocks.Count == 0) throw new ArgumentException("There is no stocks !");
            if (Beams.Count == 0) throw new ArgumentException("There is no beams !");
            if (Beam == null) throw new ArgumentNullException("You must set the beam profile on solve in based on");
            Population population = new Population();
            for (int i = 0; i < totPop; i++)
            {
#if DEBUG
                //Debug.WriteLine("************************************");
                //Debug.WriteLine("Create new Chromosome");
#endif
                var chromosome = CreateChromosome();

                population.Solutions.Add(chromosome);
            }
            return population;
        }

        private Chromosome CreateChromosome()
        {
            var cuttingStock = Stocks.ToArray();
            var beams = Beams;
            Chromosome chromosome = new Chromosome();
            for (int i = 0; i < cuttingStock.Length; i++)
            {
                var stock = cuttingStock[i];
                var cutplan = new CutPlan(i, stock.Length, beam);

                for (int j = 0; j < beams.Count; j++)
                {
                    var item = beams[j];
                    int v = (int)System.Math.Floor((1.0 * stock.Length) / (item.Length + 2 * CuttingWidth));
                    v = System.Math.Min(v, item.Need);
                    if (cutplan.AddCut(j, v, item.Length))
                        item.Need -= v;
                }

                chromosome.Genes.Add(new Gene(cutplan));
#if DEBUG
                //Debug.WriteLine("Creating gene");
                //Debug.WriteLine(cutplan);
#endif
            }
#if DEBUG
            int reste = 0;
            beams.ToList().ForEach(p => { reste += p.Need; /*Debug.WriteLine(p);*/ });
            //Debug.WriteLine("Reste :" + reste);
#endif
            beams.ToList().ForEach(item => item.Reset());
            chromosome.Genes.ShuffleFast();
            return chromosome;
        }
    }

    /* public class SolverParameter : ISolverParameter
     {
         public int ElitePercentage { get; private set; } = 2;
         public double CrossoverProbability { get; private set; } = 0.085;
         public double MutationProbability { get; private set; } = 0.001;
         public int MaxEvaluation { get; private set; } = 200;
         public int InitialPopulationCount { get; private set; } = 100;
     }*/
}
