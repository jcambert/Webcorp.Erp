using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Diagnostics;
using Webcorp.Business;
using Webcorp.Dal;
using Webcorp.Model;
using Webcorp.Controller;
using System.Threading.Tasks;

namespace Webcorp.erp.tests
{
    /// <summary>
    /// Description résumée pour TestCommandes
    /// </summary>
    [TestClass]
    public class TestCommandes
    {
        public TestCommandes()
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
            kernel = Helper.CreateKernel(new TestModule(), new BusinessIoc(), new DalIoc());

            var auth = kernel.Get<IAuthenticationService>();
            var uh = kernel.Get<IUtilisateurBusinessHelper<Utilisateur>>();
            uh.DeleteAll().Wait();

            uh.Create("999", "jcambert", "korben90", "Ambert", "Jean-Christophe", "jc.ambert@gmail.com")

                .ContinueWith(x =>
                {
                    uh.AddRole(x.Result, "Administrateur");
                })
                .ContinueWith(x =>
                {
                    uh.Save();
                }).ContinueWith(x =>
                {
                    var islogin = auth.Login("999", "jcambert", "korben90");
                    Assert.IsTrue(islogin.Result);

                }).Wait()
                ;
            Helper.CreateStandardPParametres(true).Wait();
            Helper.CreateStandardParametres(true).Wait();

        }
        [TestMethod]
        public async Task TestCommande1()
        {


            try {

                var ch = kernel.Get<ICommandeBusinessHelper<Commande>>();
                ch.DeleteAll().Wait();


                Societe s = new Societe() { Nom = "Societe test" };

                var cde = await ch.Create(CommandeType.Client);
                Assert.IsNotNull(cde);

                //cde.Numero = 1234;
                cde.Tiers = s;

                cde = await ch.Create(CommandeType.Fournisseur);
                Assert.IsNotNull(cde);

                ch.Save().Wait();
            }catch(Exception )
            {

            }
        }
        [TestMethod]
        public async Task TestCreateCommandeClient1()
        {
            var ch = kernel.Get<ICommandeBusinessHelper<Commande>>();
            ch.DeleteAll().Wait();

            var cde = await ch.Create(CommandeType.Client);
            

            Assert.IsNotNull(cde);
        }

        [TestMethod]
        public async Task TestCommande2()
        {


          //  try
          //  {

                var ch = kernel.Get<ICommandeBusinessHelper<Commande>>();
                var bh = kernel.Get<IArticleBusinessHelper<Article>>();
                ch.DeleteAll().Wait();
                bh.DeleteAll().Wait();

              
                var pf = await bh.Create("PF",ArticleType.ProduitFini);
                var sf = await bh.Create("SF",ArticleType.ProduitSemiFini);
                var ssf = await bh.Create("SSF",ArticleType.ProduitSemiFini);
                
                pf.Libelle = "Libelle PF";
                sf.Libelle = "Libelle SF";
                ssf.Libelle = "Libelle SSF";
                bh.Save().Wait();

                sf.Nomenclatures.Add(new Nomenclature(ssf) { Ordre = 20, Libelle = ssf.Libelle, Quantite = 20 });
                pf.Nomenclatures.Add(new Nomenclature(sf) { Ordre = 10, Libelle = sf.Libelle, Quantite = 10 });

                 bh.Save().Wait();

                pf.Libelle = "Other Libelle PF";
                bh.Save().Wait();

                Societe s = new Societe() { Nom = "Societe test" };

                var cde = await ch.Create(CommandeType.Client);
                Assert.IsNotNull(cde);

       
                cde.Tiers = s;

                cde.Lignes.Add(new LigneCommande() { ArticleInner = pf, QuantiteCommandee = 10, Prixunitaire = new unite.Currency(15.5) });

                ch.Save().Wait();


           /* }
            catch (Exception ex)
            {
                if (Debugger.IsAttached) Debugger.Break();
            }*/
        }

        [TestMethod,ExpectedException(typeof(InvalidOperationException))]
        public async Task TestCommandClientAvecArticleSST1()
        {
            var ch = kernel.Get<ICommandeBusinessHelper<Commande>>();
            var bh = kernel.Get<IArticleBusinessHelper<Article>>();
            ch.DeleteAll().Wait();
            bh.DeleteAll().Wait();


          
            var sst = await bh.Create("ArticleST",ArticleType.SousTraitance);
            sst.Libelle = "Peinture";
            var result=await sst.Save();
            Assert.IsTrue(result==1);

            var cde = await ch.Create(CommandeType.Client);
            var result0= await ch.Save();
            Assert.IsTrue(result==1);

            var ligne = new LigneCommande(cde) { ArticleInner = sst, QuantiteCommandee = 5, Prixunitaire = new unite.Currency(5) };
            cde.Lignes.Add(ligne);

            ch.Save().Wait();
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public async Task TestCommandeFournisseurAvecArticleNonSST1()
        {
            var ch = kernel.Get<ICommandeBusinessHelper<Commande>>();
            var bh = kernel.Get<IArticleBusinessHelper<Article>>();
            ch.DeleteAll().Wait();
            bh.DeleteAll().Wait();



            var sst = await bh.Create("ArticleST",ArticleType.ProduitFini);
            sst.Libelle = "Peinture";
            var result = await sst.Save();
            Assert.IsTrue(result==1);

            var cde = await ch.Create(CommandeType.Fournisseur);
            var result0 = await ch.Save();
            Assert.IsTrue(result==1);

            var ligne = new LigneCommande(cde) { ArticleInner = sst, QuantiteCommandee = 5, Prixunitaire = new unite.Currency(5) };
            cde.Lignes.Add(ligne);

            ch.Save().Wait();
        }

        [TestMethod]
        public async Task TestCommandeFournisseurAvecArticleSST1()
        {
            var ch = kernel.Get<ICommandeBusinessHelper<Commande>>();
            var bh = kernel.Get<IArticleBusinessHelper<Article>>();
            ch.DeleteAll().Wait();
            bh.DeleteAll().Wait();



            var sst = await bh.Create("ArticleST",ArticleType.SousTraitance);
            sst.Libelle = "Peinture";
            var result = await sst.Save();
            Assert.IsTrue(result==1);

            var mp = await bh.Create("ArticleST",ArticleType.MatièrePremiere);
            mp.Libelle = "Peinture";
            var result0 = await sst.Save();
            Assert.IsTrue(result0==1);

            var cde = await ch.Create(CommandeType.Fournisseur);
            //var result1 = await ch.Save();
            //  Assert.IsTrue(!result1.HasError());
            ch.Save().Wait();

            Assert.IsTrue(cde.Id != "");

            var ligne0 = new LigneCommande(cde) { ArticleInner = sst, QuantiteCommandee = 5, Prixunitaire = new unite.Currency(5) };
            cde.Lignes.Add(ligne0);

            var ligne1 = new LigneCommande(cde) { ArticleInner = mp, QuantiteCommandee = 15, Prixunitaire = new unite.Currency(500) };
            cde.Lignes.Add(ligne1);

            ch.Save().Wait();
        }
    }


}
