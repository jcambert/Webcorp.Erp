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
using System.Threading.Tasks;
using Webcorp.Model;
using Webcorp.Model.Quotation;

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
        public void TestCuttingPopulation()
        {
            var kernel = new StandardKernel(new OneDCutModule());

            kernel.Bind<IPopulation>().To<InitialBeamPopulationWithNoError>().InSingletonScope();
            kernel.Bind(typeof(IEntityProvider<,>)).To(typeof(EntityProvider<,>)).InSingletonScope();
            kernel.Bind(typeof(IEntityProviderInitializable<Article, string>)).To(typeof(BeamInitializer));
            var population = kernel.Get<IPopulation>();
            Assert.IsTrue(population.CuttingStock.Count() == 126);
            Assert.IsTrue(population.CuttingStock.Length == 4125);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Must have same length")]
        public void TestCuttingPopulationError()
        {
            var kernel = new StandardKernel(new OneDCutModule());

            kernel.Bind<IPopulation>().To<InitialBeamPopulationWithError>().InSingletonScope();
            var population = kernel.Get<IPopulation>();
        }

        [TestMethod]
        public void TestCuttingBeamWithNinject()
        {
            var kernel = new StandardKernel(new OneDCutModule());

            kernel.Bind<IPopulation>().To<InitialBeamPopulation>().InSingletonScope();
            kernel.Bind(typeof(IEntityProvider<,>)).To(typeof(EntityProvider<,>)).InSingletonScope();
            kernel.Bind(typeof(IEntityProviderInitializable<Article, string>)).To(typeof(BeamInitializer));

            ISolver solver = kernel.Get<ISolver>();
            Assert.IsNotNull(solver);
            solver.Beams = kernel.Get<IPopulation>().Beams;
            solver.Stocks = kernel.Get<IPopulation>().CuttingStock;
            solver.Beam = kernel.Get<IPopulation>().Beam;
            solver.OnSolved += Solver_OnSolved;
            solver.Solve();
        }

        [TestMethod]
        public async Task TestCuttingBeamWithNinjectAsync()
        {
            var kernel = new StandardKernel(new OneDCutModule());

            kernel.Bind<IPopulation>().To<InitialBeamPopulation>().InSingletonScope();

            kernel.Bind(typeof(IEntityProvider<,>)).To(typeof(EntityProvider<,>)).InSingletonScope();
            kernel.Bind(typeof(IEntityProviderInitializable<Article, string>)).To(typeof(BeamInitializer));
            ISolver solver = kernel.Get<ISolver>();
            Assert.IsNotNull(solver);
            solver.Beams = kernel.Get<IPopulation>().Beams;
            solver.Stocks = kernel.Get<IPopulation>().CuttingStock;
            solver.Beam = kernel.Get<IPopulation>().Beam;
            solver.OnSolved += Solver_OnSolved;
            await solver.SolveAsync();
        }


        [TestMethod]
        public void TestCuttingBeamWithNinjectWithBigData()
        {
            var kernel = new StandardKernel(new OneDCutModule());

            kernel.Bind<IPopulation>().To<InitialBeamBigPopulation>().InSingletonScope();
            kernel.Bind(typeof(IEntityProvider<,>)).To(typeof(EntityProvider<,>)).InSingletonScope();
            kernel.Bind(typeof(IEntityProviderInitializable<Article, string>)).To(typeof(BeamInitializer));
            ISolver solver = kernel.Get<ISolver>();
            Assert.IsNotNull(solver);
            solver.Beams = kernel.Get<IPopulation>().Beams;
            solver.Stocks = kernel.Get<IPopulation>().CuttingStock;
            solver.Beam = kernel.Get<IPopulation>().Beam;

            solver.OnSolved += Solver_OnSolved;
            solver.Solve();
        }

        [TestMethod]
        public void TestCuttingBeamRealCase()
        {
            var kernel = new StandardKernel(new OneDCutModule());

            kernel.Bind<IPopulation>().To<InitialBeamRealPopulation>().InSingletonScope();
            kernel.Bind(typeof(IEntityProvider<,>)).To(typeof(EntityProvider<,>)).InSingletonScope();
            kernel.Bind(typeof(IEntityProviderInitializable<Article, string>)).To(typeof(BeamInitializer));
            ISolver solver = kernel.Get<ISolver>();
            Assert.IsNotNull(solver);
            solver.Beams = kernel.Get<IPopulation>().Beams;
            solver.Stocks = kernel.Get<IPopulation>().CuttingStock;
            solver.Beam = kernel.Get<IPopulation>().Beam;

            solver.OnSolved += Solver_OnSolved;
            solver.Solve();
        }

        private void Solver_OnSolved(object sender, SolverEventArgs e)
        {
            var solver = sender as Solver;
            Debug.WriteLine("Best solution");
            Debug.WriteLine("----------------------------");
            Debug.WriteLine("Total To Cut:" + e.TotalToCut);
            Debug.WriteLine("Total Cut:" + e.TotalCut);
            Debug.WriteLine("Total Stock:" + e.TotalStock);
            Debug.WriteLine("Total Cutting Mass:" + e.TotalCuttingMass.ToString("#.00 [kg]"));
            Debug.WriteLine("Total Cutting Cost:" + e.TotalCuttingCost.ToString("#.00 [euro]"));
            Debug.WriteLine("Total Waste Length:" + e.TotalWaste);
            Debug.WriteLine("Total Waste Cost:" + e.TotalWasteCost.ToString("#.00 [euro]"));
            Debug.WriteLine(string.Format("Percentage Waste {0:P2} ", e.WastePercentage));
            Debug.WriteLine("Uncut Stock:" + e.TotalUncut);

            if (!e.IsStockSuffisant)
            {
                Debug.WriteLine("No stock to cut all need");
                Debug.WriteLine(e.RestToCut + " rest tot cut");
            }
            DebugCutPlan(e.CutPlan);
        }

        private void DebugCutPlan(List<lib.onedcut.CutPlan> cutplan)
        {

            Debug.WriteLine("**** Cutting plan ****");
            foreach (var cut in cutplan.Where(c=>c.CutLength>0).OrderBy(i => i.StockIndex))
            {
                Debug.WriteLine(cut);
            }
        }



    }

    public class InitialBeamPopulation : IPopulation, IInitializable
    {
        Stocks stocks;
        Beams beams = new Beams();
        public InitialBeamPopulation()
        {

        }


        public ReactiveList<BeamToCut> Beams => beams;

        public Stocks CuttingStock => stocks;

        [Inject]
        public IKernel Kernel { get; set; }

        Article _beam;
        public Article Beam => _beam;


        public void Initialize()
        {
            var mpp = Kernel.Get<IEntityProvider<Article, string>>();
            _beam = mpp.Find("IPE 220");
            _beam.MassCurrency = unite.MassCurrency.Parse("600 euro/tonne");
            for (int i = 0; i < 10; i++)
            {
                beams.Add(new BeamToCut(3, 5, _beam));
                beams.Add(new BeamToCut(5, 1, _beam));
                beams.Add(new BeamToCut(5, 2, _beam));
            }
            int[] cuttingStock = new int[] { 20, 5, 105, 150, 30 };
            stocks = new Stocks(cuttingStock);
        }
    }

    public class InitialBeamPopulationWithError : IPopulation, IInitializable
    {

        public InitialBeamPopulationWithError()
        {

        }

        public Article Beam => null;

        public ReactiveList<BeamToCut> Beams => null;

        public Stocks CuttingStock => null;

        public void Initialize()
        {

            int[] cuttingStockCount = new int[] { 1, 1, 1, 1 };
            int[] cuttingStockLength = new int[] { 20, 5, 105, 150, 30 };
            var stocks = new Stocks(cuttingStockCount, cuttingStockLength);
        }
    }

    public class InitialBeamPopulationWithNoError : IPopulation, IInitializable
    {

        public InitialBeamPopulationWithNoError()
        {

        }

        public Article Beam => null;

        public ReactiveList<BeamToCut> Beams => null;

        public Stocks CuttingStock
        {
            get; private set;
        }

        public void Initialize()
        {

            int[] cuttingStockCount = new int[] { 10, 8, 7, 1, 100 };
            int[] cuttingStockLength = new int[] { 20, 5, 105, 150, 30 };
            CuttingStock = new Stocks(cuttingStockLength, cuttingStockCount);
        }
    }


    public class InitialBeamBigPopulation : IPopulation, IInitializable
    {
        Stocks stocks;
        Beams beams = new Beams();
        public InitialBeamBigPopulation()
        {

        }


        public ReactiveList<BeamToCut> Beams => beams;

        public Stocks CuttingStock => stocks;

        [Inject]
        public IKernel Kernel { get; set; }

        Article _beam;
        public Article Beam => _beam;


        public void Initialize()
        {
            var mpp = Kernel.Get<IEntityProvider<Article, string>>();
            _beam = mpp.Find("IPE 80");
            _beam.MassCurrency = unite.MassCurrency.Parse("600 euro/tonne");
            for (int i = 0; i < 1000; i++)
            {
                beams.Add(new BeamToCut(3, 5, _beam));
                beams.Add(new BeamToCut(5, 1, _beam));
                beams.Add(new BeamToCut(5, 2, _beam));
                beams.Add(new BeamToCut(20, 15, _beam));
                beams.Add(new BeamToCut(15, 10, _beam));
                beams.Add(new BeamToCut(50, 20, _beam));


            }
            int[] cuttingStockCount = new int[] { 100, 200, 80, 1000, 100 };
            int[] cuttingStockLength = new int[] { 200, 50, 1050, 1500, 300 };
            stocks = new Stocks(cuttingStockLength, cuttingStockCount);
        }
    }


    public class InitialBeamRealPopulation : IPopulation, IInitializable
    {
        Stocks stocks;
        Beams beams = new Beams();
        public InitialBeamRealPopulation()
        {

        }


        public ReactiveList<BeamToCut> Beams => beams;

        public Stocks CuttingStock => stocks;

        [Inject]
        public IKernel Kernel { get; set; }

        Article _beam;
        public Article Beam => _beam;


        public void Initialize()
        {
            var mpp = Kernel.Get<IEntityProvider<Article, string>>();
            _beam = mpp.Find("IPE 80");
            _beam.MassCurrency = unite.MassCurrency.Parse("600 euro/tonne");

            beams.Add(new BeamToCut(5,4024, _beam));
           // beams.Add(new BeamToCut(4,1896, _beam));
            //beams.Add(new BeamToCut(2, 4456, _beam));
            
            stocks = new Stocks(6000,100);
        }
    }


}
