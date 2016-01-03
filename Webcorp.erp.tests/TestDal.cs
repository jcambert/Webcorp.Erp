using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Webcorp.Dal;
using Webcorp.Model;
using System.Threading.Tasks;
using System.Linq;
namespace Webcorp.erp.tests
{
    /// <summary>
    /// Description résumée pour TestDal
    /// </summary>
    [TestClass]
    public class TestDal
    {
        public TestDal()
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
        static IKernel kernel;
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = new StandardKernel(new TestModule(), new DalIoc());
        }
        #endregion


        [TestMethod]
        public async Task TestDal1()
        {
            var repo = kernel.Get<IRepository<Societe>>();
            await repo.DeleteAll();
            Assert.AreEqual(await repo.CountAll(), 0);

            await repo.Upsert(new Societe() { Nom = "Soc test",Adresse=new Addresse() });

            Assert.AreEqual(await repo.CountAll(), 1);

            
            
        }
    }
}
