using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Webcorp.lib.onedcut;
using GAF;
using GAF.Extensions;
using System.Diagnostics;
using ReactiveUI;

namespace Webcorp.oned.tests
{
    /// <summary>
    /// Description résumée pour UnitTest3
    /// </summary>
    [TestClass]
    public class UnitTest3
    {
        public UnitTest3()
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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {

        }


        [TestMethod]
        public void TestCuttingBeamWithNinject()
        {
            var kernel = new StandardKernel(new OneDCutModule());

            kernel.Bind<IPopulation>().To<InitialBeamPopulation>().InSingletonScope();
            ISolver solver = kernel.Get<ISolver>();
            Assert.IsNotNull(solver);
            solver.Beams = kernel.Get<IPopulation>().Beams;

            solver.OnSolved += Solver_OnSolved;
            solver.Solve();
        }

        private void Solver_OnSolved(object sender, SolverEventArgs e)
        {
            var solver = sender as Solver;
            var fittest = e.Population.GetTop(1)[0];
            Debug.WriteLine("Fittest chromosome or best solution");
            DebugChromosome(fittest);
            int totCut = 0, totUncut = 0, totWaste = 0, totStock = 0, totToCut = 0;
            foreach (var gene in fittest.Genes)
            {
                var cutplan = (gene.ObjectValue as Webcorp.lib.onedcut.CutPlan);
                totWaste += cutplan.Waste == cutplan.StockLength ? 0 : cutplan.Waste;
                totStock += cutplan.Waste == cutplan.StockLength ? 0 : cutplan.StockLength;
                totCut += cutplan.CutLength;
                totUncut += cutplan.Waste == cutplan.StockLength ? cutplan.StockLength : 0;
            }

            solver.Beams.ToList().ForEach(item => totToCut += item.TotalLength);
            Debug.WriteLine("Total To Cut:" + totToCut);
            Debug.WriteLine("Total Cut:" + totCut);
            Debug.WriteLine("Total Waste:" + totWaste);
            Debug.WriteLine("Total Stock:" + totStock);
            Debug.WriteLine(string.Format("Percentage Waste {0:P2} ", (totWaste * 1.0 / totStock)));
            Debug.WriteLine("Uncut Stock:" + totUncut);

            if (totToCut > totCut)
            {
                Debug.WriteLine("No stock to cut all need");
                Debug.WriteLine(totToCut - totCut + " rest tot cut");
            }
        }

        private void DebugChromosome(Chromosome c)
        {
            foreach (var gene in c.Genes)
            {
                Debug.WriteLine(gene.ObjectValue as Webcorp.lib.onedcut.CutPlan);
            }
        }
    }

    public class InitialBeamPopulation : IPopulation
    {
        Stocks stocks;
        Beams beams = new Beams();
        public InitialBeamPopulation()
        {
            for (int i = 0; i < 10; i++)
            {
                beams.Add(new Beam(3, 5));
                beams.Add(new Beam(5, 1));
                beams.Add(new Beam(5, 2));
            }
            int[] cuttingStock = new int[] { 20, 5, 105, 150, 30 };
            stocks = new Stocks(cuttingStock);
        }
       

        public ReactiveList<Beam> Beams => beams;

        public ReactiveList<BeamStock> CuttingStock => stocks;
        
        

        /*    private Population CreateInitialePopulation(int totPop)
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
                    var cutplan = new Webcorp.lib.onedcut.CutPlan(i,stock);

                    for (int j = 0; j < beams.Count; j++)
                    {
                        var item = beams[j];
                        int v = (int)System.Math.Floor((1.0 * stock) / item.Length);
                        v = System.Math.Min(v, item.Need);
                        if (cutplan.AddCut(j, v, item.Length))
                            item.Need -= v;
                    }

                    chromosome.Genes.Add(new Gene(cutplan));
                    Debug.WriteLine("Creating gene");
                    Debug.WriteLine(cutplan);
                }
                int reste = 0;
                beams.ForEach(p => { reste += p.Need; Debug.WriteLine(p); });
                Debug.WriteLine("Reste :" + reste);

                beams.ForEach(item => item.Reset());
                chromosome.Genes.ShuffleFast();
                return chromosome;
            }
      */
    }
}
