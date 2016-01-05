using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Webcorp.Controller;
using Webcorp.Model.Quotation;
using Webcorp.Model;
using System.Reflection;
using Webcorp.Business;
using Webcorp.Dal;
using System.Threading.Tasks;

namespace Webcorp.erp.tests
{
    /// <summary>
    /// Description résumée pour TestBusiness
    /// </summary>
    [TestClass]
    public class TestBusiness
    {
        static IKernel kernel;

        public TestBusiness()
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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = new StandardKernel(new TestModule(),new BusinessIoc(),new DalIoc());
        }

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

        [TestMethod]
        public async Task TestBusiness1()
        {
           
            //var bap = kernel.Get<IBusinessAssemblyProvider>();
            //bap.Assemblies.Add(Assembly.GetAssembly(typeof(AbstractBusiness<>)));
           // var busprov = kernel.Get<IBusinessProvider<Material>>();
            var bhelper = kernel.Get<IArticleBusinessHelper<Article>>();


            var art =await bhelper.Create("Temp material",ArticleType.MatièrePremiere);
            

            Assert.IsTrue(art.IsChanged);

           

            await bhelper.Save();

            Assert.IsFalse(art.IsChanged);

            art.MouvementsStocks.Add(new MouvementStock() { Date = DateTime.Parse("1/1/2015"), Quantite = 10, Sens = MouvementSens.Entree });
            Assert.IsTrue(art.IsChanged);
            await bhelper.Save();


            art.MouvementsStocks.Add(new MouvementStock() { Date = DateTime.Parse("2/1/2015"), Quantite = 7, Sens = MouvementSens.Sortie });
        }

        [TestMethod]
        public async Task TestArticleFraisGeneraux()
        {
            var bh = kernel.Get<IArticleBusinessHelper<Article>>();
            var ctrl = kernel.Get<IBusinessController<Article>>();
            var art=await bh.Create("FAD",ArticleType.FraisGeneraux);
            art.Libelle = "Frais Administratif";
            art.Tarif0 = new unite.Currency("45 euro");
            await bh.Save();

            art.Tarif0 =art.Tarif0+ 20.0;

            await bh.Save();

            var ret =await ctrl.Get(art.Id);

            Assert.AreEqual(ret.Tarif0, new unite.Currency(45 + 20));
        }

        [TestMethod]
        public void TestArticleProduitFini()
        {
            var bh = kernel.Get<IArticleBusinessHelper<Article>>();
            var ctrl = kernel.Get<IBusinessController<Article>>();
            var art = bh.Create("",ArticleType.ProduitFini);
        }
    }
}
