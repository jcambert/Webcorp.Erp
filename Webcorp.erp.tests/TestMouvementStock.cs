using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Diagnostics;
using Webcorp.Model;
using Webcorp.Model.Quotation;
using Webcorp.Controller;
using Webcorp.Business;
using Webcorp.unite;
using Webcorp.Dal;
using System.Threading.Tasks;

namespace Webcorp.erp.tests
{
    /// <summary>
    /// Description résumée pour TestMouvementStock
    /// </summary>
    [TestClass]
    public class TestMouvementStock
    {
        public TestMouvementStock()
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
        static IKernel kernel;



        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            Debug.WriteLine("ClassInit " + context.TestName);
            kernel = new StandardKernel(new TestModule(),new BusinessIoc(),new DalIoc());

        }
        [TestMethod]
        public void TestMouvementStock1()
        {
           
            kernel.Bind(typeof(IEntityProviderInitializable<Article, string>)).To(typeof(BeamInitializer));

            var mpp = kernel.Get<IEntityProvider<Article, string>>();
            var bh = kernel.Get<IArticleBusinessHelper<Article>>();
            var _beam = mpp.Find("IPE 80");
            _beam.MouvementsStocks.Clear();
            bh.Attach(_beam);
            _beam.MouvementsStocks.Add(new MouvementStock() { Date = DateTime.Parse("1/1/2015"), Quantite = 10, Sens = MouvementSens.Entree });
            _beam.MouvementsStocks.Add(new MouvementStock() { Date = DateTime.Parse("2/1/2015"), Quantite = 7, Sens = MouvementSens.Sortie });

            Assert.AreEqual(_beam.StockPhysique, 3);
        }

        [TestMethod]
        public void TestMouvementStock2()
        {

            kernel.Bind(typeof(IEntityProviderInitializable<Article, string>)).To(typeof(BeamInitializer));

            var mpp = kernel.Get<IEntityProvider<Article, string>>();
            var bh = kernel.Get<IArticleBusinessHelper<Article>>();
            var _beam = mpp.Find("IPE 80");
            _beam.MouvementsStocks.Clear();
            bh.Attach(_beam);
            _beam.MouvementsStocks.Add(new MouvementStock() { Date = DateTime.Parse("2/1/2015"), Quantite = 10, Sens = MouvementSens.Entree });
            _beam.MouvementsStocks.Add(new MouvementStock() { Date = DateTime.Parse("1/1/2015"), Quantite = 7, Sens = MouvementSens.Sortie });

            Assert.AreEqual(_beam.StockPhysique, 3);
        }

        [TestMethod]
        public void TestMouvementStock3()
        {

            kernel.Bind(typeof(IEntityProviderInitializable<Article, string>)).To(typeof(BeamInitializer));

            var mpp = kernel.Get<IEntityProvider<Article, string>>();
            var bh = kernel.Get<IArticleBusinessHelper<Article>>();
            var _beam = mpp.Find("IPE 80");
            _beam.MouvementsStocks.Clear();
            bh.Attach(_beam);
            _beam.MouvementsStocks.Add(new MouvementStock() { Date = DateTime.Parse("2/1/2015"), Quantite = 10, Sens = MouvementSens.Entree });
            _beam.MouvementsStocks.Add(new MouvementStock() { Date = DateTime.Parse("1/1/2015"), Quantite = 7, Sens = MouvementSens.Sortie });

            Assert.AreEqual(_beam.StockPhysique, 3);
            _beam.MouvementsStocks.Add(new MouvementStock() { Date = DateTime.Parse("10/1/2015"), Quantite = 50, Sens = MouvementSens.Inventaire });
            Assert.AreEqual(_beam.StockPhysique, 50);
        }


        [TestMethod]
        public async Task TestMouvementStock4()
        {

            kernel.Bind(typeof(IEntityProviderInitializable<Article, string>)).To(typeof(BeamInitializer));

            var mpp = kernel.Get<IEntityProvider<Article, string>>();
            var bh = kernel.Get<IArticleBusinessHelper<Article>>();
            var _beam = mpp.Find("IPE 80");
            _beam.MouvementsStocks.Clear();
            bh.Attach(_beam);
            _beam.MouvementsStocks.Add(new MouvementStock() { Date = DateTime.Parse("2/1/2015"), Quantite = 10, Sens = MouvementSens.Entree });
            _beam.MouvementsStocks.Add(new MouvementStock() { Date = DateTime.Parse("10/1/2015"), Quantite = 7, Sens = MouvementSens.Sortie });

            Assert.AreEqual(_beam.StockPhysique, 3);
            _beam.MouvementsStocks.Add(new MouvementStock() { Date = DateTime.Parse("5/1/2015"), Quantite = 50, Sens = MouvementSens.Inventaire });
            Assert.AreEqual(_beam.StockPhysique, 43);

            Assert.AreEqual(_beam.MouvementsStocks.StockAtDate(DateTime.Parse("2/1/2015")), 10);

           await bh.Save();
        }

        [TestMethod]
        public /*async Task*/ void TestMouvementStock5()
        {

            kernel.Bind(typeof(IEntityProviderInitializable<Article, string>)).To(typeof(BeamInitializer));

            var mpp = kernel.Get<IEntityProvider<Article, string>>();
            var bh = kernel.Get<IArticleBusinessHelper<Article>>();
            var _beam = mpp.Find("IPE 80");
            _beam.MouvementsStocks.Clear();
            bh.Attach(_beam);
            _beam.MouvementsStocks.Add(new MouvementStock() { Date = DateTime.Parse("1/1/2015"), Quantite = 10, Sens = MouvementSens.Entree });
            Assert.AreEqual(_beam.StockPhysique, 10);
            _beam.MouvementsStocks.Add(new MouvementStock() { Date = DateTime.Parse("2/1/2015"), Quantite = 7, Sens = MouvementSens.Sortie });
            Assert.AreEqual(_beam.StockPhysique, 3);
            _beam.MouvementsStocks.Add(new MouvementStock() { Date = DateTime.Parse("3/1/2015"), Quantite = 5, Sens = MouvementSens.Entree });
            Assert.AreEqual(_beam.StockPhysique, 8);
            _beam.MouvementsStocks.Add(new MouvementStock() { Date = DateTime.Parse("4/1/2015"), Quantite = 20, Sens = MouvementSens.Sortie });
            Assert.AreEqual(_beam.StockPhysique, -12);

            _beam.MouvementsStocks.RemoveAt(1);
            Assert.AreEqual(_beam.StockPhysique, -5);

            _beam.MouvementsStocks.RemoveAt(0);
            Assert.AreEqual(_beam.StockPhysique, -15);

            _beam.MouvementsStocks.RemoveAt(1);
            Assert.AreEqual(_beam.StockPhysique, 5);

            
            //await bh.Save();
        }
    }
}
