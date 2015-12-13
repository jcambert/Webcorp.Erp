using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Diagnostics;
using Webcorp.Model;
using Webcorp.Model.Quotation;
using Webcorp.Controller;

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
            kernel = new StandardKernel(new TestModule());

        }
        [TestMethod]
        public void TestMouvementStock1()
        {
            kernel.Bind(typeof(IEntityProvider<,>)).To(typeof(EntityProvider<,>)).InSingletonScope();
            kernel.Bind(typeof(IEntityProviderInitializable<Article, string>)).To(typeof(BeamInitializer));

            var mpp = kernel.Get<IEntityProvider<Article, string>>();
            var bh = kernel.Get<IBusinessHelper<Article>>();
            var _beam = mpp.Find("IPE 80");
            Assert.IsNotNull(_beam);
            bh.Attach(_beam);

            _beam.MouvementsStocks.Add()
        }
    }
}
