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
            var bhelper = kernel.Get<IArticleBusinessHelper<Material>>();


            var material = bhelper.Create();
            material.Code = "Temp material";
            material.Density = new unite.Density(3);

            Assert.IsTrue(material.IsChanged);

            Assert.AreEqual(material.Density.Value, (3.0 / 2));

            await bhelper.Save();

            Assert.IsFalse(material.IsChanged);

             material.MouvementsStocks.Add(new MouvementStock() { Date = DateTime.Parse("1/1/2015"), Quantite = 10, Sens = MouvementSens.Entree });
            Assert.IsTrue(material.IsChanged);
            await bhelper.Save();


            material.MouvementsStocks.Add(new MouvementStock() { Date = DateTime.Parse("2/1/2015"), Quantite = 7, Sens = MouvementSens.Sortie });
        }

       
    }
}
