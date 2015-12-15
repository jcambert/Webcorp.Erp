using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Diagnostics;
using Webcorp.Business;
using Webcorp.Dal;
using Webcorp.Model;
using Webcorp.Model.Quotation;
using System.Threading.Tasks;

namespace Webcorp.erp.tests
{
    /// <summary>
    /// Description résumée pour TestNomenclature
    /// </summary>
    [TestClass]
    public class TestNomenclature
    {
        public TestNomenclature()
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
            kernel = new StandardKernel(new TestModule(), new BusinessIoc(), new DalIoc());
            kernel.Bind(typeof(IEntityProviderInitializable<Article, string>)).To(typeof(BeamInitializer));
        }
        [TestMethod]
        public async Task TestNomenclature1()
        {
            var mpp = kernel.Get<IEntityProvider<Article, string>>();
            var bh = kernel.Get<IArticleBusinessHelper<Article>>();
            var pf = bh.Create();
            var sf = bh.Create();
            pf.Code = "PF";
            pf.Libelle = "Libelle PF";
            sf.Code = "SF";
            sf.Libelle = "Libelle SF";
            await bh.Save();

            pf.Nomenclatures.Add(new Nomenclature() { Ordre = 10,Article=sf, Libelle = sf.Libelle, Quantite = 10 });

            await bh.Save();
        }
    }
}
