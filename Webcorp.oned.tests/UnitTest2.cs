using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GAF;
using System.Diagnostics;
using GAF.Operators;
using GAF.Extensions;
namespace Webcorp.oned.tests
{
    /// <summary>
    /// Description résumée pour UnitTest2
    /// </summary>
    [TestClass]
    public class UnitTest2
    {
        public UnitTest2()
        {
            //
            // TODO: ajoutez ici la logique du constructeur
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Obtient ou définit le contexte de test qui fournit
        ///des informations sur la série de tests active, ainsi que ses fonctionnalités.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Attributs de tests supplémentaires
        //
        // Vous pouvez utiliser les attributs supplémentaires suivants lorsque vous écrivez vos tests :
        //
        // Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test de la classe
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Utilisez ClassCleanup pour exécuter du code une fois que tous les tests d'une classe ont été exécutés
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
        int[] cuttingStock = new int[] { 20,5,105,150,30};
        List<Item> items = new List<Item>();
        Random random = new Random();
        [TestMethod]
        public void TestMethod1()
        {
            /*
            Chromosome representation
            StockLength*item
            */
            for (int i = 0; i < 10; i++)
            {
                items.Add(new Item(3, 5));
                items.Add(new Item(5, 1));
                items.Add(new Item(5, 2));
            }
            


            var population = CreateInitialePopulation(500);


            var elite = new Elite(2);
            var crossover = new Crossover(0.85) { CrossoverType = CrossoverType.DoublePointOrdered };
            var mutate = new SwapMutate(0.001);
            var ga = new GeneticAlgorithm(population, CalculateFitness);
            ga.OnGenerationComplete += Ga_OnGenerationComplete;
            ga.OnRunComplete += Ga_OnRunComplete;
            ga.Operators.Add(elite);
            ga.Operators.Add(crossover);
            ga.Operators.Add(mutate);
            ga.Run(200);
        }

        private double CalculateFitness(Chromosome solution)
        {
            int totWaste=0, totStock=0;
            foreach (var gene in solution.Genes)
            {
                totWaste+= (gene.ObjectValue as CutPlan).Waste;
                totStock += (gene.ObjectValue as CutPlan).StockLength;
            }
            return ( totWaste / totStock);
        }

        private void Ga_OnRunComplete(object sender, GaEventArgs e)
        {
            var fittest = e.Population.GetTop(1)[0];
            Debug.WriteLine("Fittest chromosome or best solution");
            DebugChromosome(fittest);
            int totCut=0,totUncut=0, totWaste = 0, totStock = 0,totToCut=0;
            foreach (var gene in fittest.Genes)
            {
                var cutplan = (gene.ObjectValue as CutPlan);
                totWaste += cutplan.Waste==cutplan.StockLength?0:cutplan.Waste;
                totStock += cutplan.Waste == cutplan.StockLength ? 0 : cutplan.StockLength;
                totCut += cutplan.CutLength;
                totUncut += cutplan.Waste == cutplan.StockLength ? cutplan.StockLength : 0;
            }
            items.ForEach(item => totToCut += item.TotalLength);
            Debug.WriteLine("Total To Cut:" + totToCut);
            Debug.WriteLine("Total Cut:" + totCut);
            Debug.WriteLine("Total Waste:" + totWaste);
            Debug.WriteLine("Total Stock:" + totStock);
            Debug.WriteLine(string.Format("Percentage Waste {0:P2} ", (totWaste*1.0 / totStock)));
            Debug.WriteLine("Uncut Stock:" + totUncut);

            if (totToCut > totCut)
            {
                Debug.WriteLine("No stock to cut all need");
                Debug.WriteLine(totToCut - totCut + " rest tot cut");
            }
            /* for (int i = 0; i < fittest.Genes.Count; i++)
             {


                 StringBuilder sb = new StringBuilder();
                 int stock = stocks[i];
                 int[] solitems = (int[])fittest.Genes[i].ObjectValue;
                 int sum = solitems.Sum();

                 Debug.WriteLine(string.Format("Stock {0} - Length {1} with {2}", i, stock, string.Join(",", solitems)));
             }*/
        }

        private void Ga_OnGenerationComplete(object sender, GaEventArgs e)
        {
            
        }

        private Population CreateInitialePopulation(int totPop)
        {
            Population population = new Population();
            for (int i = 0; i < totPop; i++)
            {
                Debug.WriteLine("************************************");
                Debug.WriteLine("Create new Chromosome");
                var chromosome = CreateChromosome();

                population.Solutions.Add(chromosome);
            }
            return population;
        }

        private Chromosome CreateChromosome()
        {
            Chromosome chromosome = new Chromosome();
            for (int i = 0; i < cuttingStock.Length; i++)
            {
                var stock = cuttingStock[i];
                var cutplan = new CutPlan();
                cutplan.StockIndex = i;
                cutplan.StockLength = stock;
                for (int j = 0; j < items.Count; j++)
                {
                    var item = items[j];
                    int v = random.Next(item.Need);
                    v = (int)System.Math.Floor((1.0*stock) / item.Length);
                    v = System.Math.Min(v, item.Need);
                    if (cutplan.AddCut(j, v, item.Length))
                        item.Need -= v;
                }

                chromosome.Genes.Add(new Gene(cutplan));
                Debug.WriteLine("Creating gene");
                Debug.WriteLine(cutplan);
            }
            int reste = 0;
            items.ForEach(p => { reste += p.Need; Debug.WriteLine(p); });
            Debug.WriteLine("Reste :" + reste);

            items.ForEach(item => item.Reset());
            chromosome.Genes.ShuffleFast();
            return chromosome;
        }
        private void DebugChromosome(Chromosome c)
        {
            foreach (var gene in c.Genes)
            {
                Debug.WriteLine(gene.ObjectValue as CutPlan);
            }
        }
    }

    class Item
    {
        private int _need;

        public Item(int need, int length)
        {
            this._need = this.Need = need;
            this.Length = length;
        }
        public int Length;

        public int Need;

        public int TotalLength => _need * Length;

        public void Reset()
        {
            Need = _need;
        }

        public override string ToString()
        {
            return "Item Need:" + Need;
        }
    }

    class CutPlan
    {
        public int StockIndex;
        public int StockLength;
        int cutLength;
        List<beam> beams = new List<beam>();

        public int Waste => StockLength - cutLength;
        public int CutLength => cutLength;
        public bool AddCut(int index, int qty, int length)
        {
            if (qty == 0) return false;
            if (qty * length + cutLength <= StockLength)
            {
                cutLength += (qty * length);
                beams.Add(new beam() { qty = qty, length = length });
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Stock Index:" + StockIndex);
            sb.AppendLine("Stock Length:" + StockLength);
            sb.AppendLine("Cutting Length:" + cutLength);
            sb.AppendLine("Cutting details");
            sb.Append("\t");
            sb.AppendLine(string.Join("\n\t", beams));
            sb.AppendLine("Waste:" + Waste);
            return sb.ToString();
        }
        class beam
        {
            public int qty;
            public int length;
            public override string ToString()
            {
                return "Quantity:" + qty + "/Length:" + length;
            }
        }
    }
}
