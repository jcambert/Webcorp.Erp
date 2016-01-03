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
using Splat;
using Webcorp.Controller;

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
            await bh.DeleteAll();
            var pf = await bh.Create(ArticleType.ProduitFini);
            var sf = await bh.Create(ArticleType.ProduitSemiFini);
            pf.Code = "PF";
            pf.Libelle = "Libelle PF";
            sf.Code = "SF";
            sf.Libelle = "Libelle SF";
            

            pf.Nomenclatures.Add(new Nomenclature(sf) { Ordre = 10, Libelle = sf.Libelle, Quantite = 10 });

            await bh.Save();
        }

        [TestMethod]
        public async Task TestNomenclature2()
        {
            

            var mpp = kernel.Get<IEntityProvider<Article, string>>();
            var bh = kernel.Get<IArticleBusinessHelper<Article>>();
            await bh.DeleteAll();
            var pf = await bh.Create(ArticleType.ProduitFini);
            var sf = await bh.Create(ArticleType.ProduitSemiFini);
            var ssf =await  bh.Create(ArticleType.ProduitSemiFini);
            pf.Code = "PF";
            pf.Libelle = "Libelle PF";
            sf.Code = "SF";
            sf.Libelle = "Libelle SF";
            ssf.Code = "SSF";
            ssf.Libelle = "Libelle SSF";
            
            await bh.Save();
            sf.Nomenclatures.Add(new Nomenclature(ssf) { Ordre = 20,  Libelle = ssf.Libelle, Quantite = 20 });
            pf.Nomenclatures.Add(new Nomenclature(sf) { Ordre = 10,  Libelle = sf.Libelle, Quantite = 10 });

            await bh.Save();

            var tmp = await bh.GetById(pf.Id);
            var nome = tmp.Nomenclatures[0];

            while (nome.Article == null) { }
            Assert.IsNotNull(nome.Article);
        }

       
         [TestMethod]
        public async Task TestNomenclature3()
        {

            var logger = new DebugLogger() { Level = LogLevel.Error };
            Locator.CurrentMutable.RegisterConstant(logger, typeof(ILogger));
          
            var mpp = kernel.Get<IEntityProvider<Article, string>>();
            var bh = kernel.Get<IArticleBusinessHelper<Article>>();
            await bh.DeleteAll();
            var pf = await bh.Create(ArticleType.ProduitFini);
            var sf = await bh.Create(ArticleType.ProduitSemiFini);
            var ssf = await bh.Create(ArticleType.ProduitSemiFini);

            

            pf.Code = "PF";
            pf.Libelle = "Libelle PF";
            sf.Code = "SF";
            sf.Libelle = "Libelle SF";
            ssf.Code = "SSF";
            ssf.Libelle = "Libelle SSF";

            sf.Nomenclatures.Add(new Nomenclature(ssf) { Ordre = 20,  Libelle = ssf.Libelle, Quantite = 20 });
            pf.Nomenclatures.Add(new Nomenclature(sf) { Ordre = 10,  Libelle = sf.Libelle, Quantite = 10 });
           
            await bh.Save();

            var ret = await bh.GetById(pf.Id);
            Assert.IsNotNull(ret);

            sf.Nomenclatures[0].Quantite = 5;
           // Assert.IsTrue(pf.IsChanged);
            await bh.Save();
        }
    }
}
