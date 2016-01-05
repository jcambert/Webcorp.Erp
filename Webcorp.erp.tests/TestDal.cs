using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Webcorp.Dal;
using Webcorp.Model;
using System.Threading.Tasks;
using System.Linq;
using Webcorp.Business;

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
            kernel = new StandardKernel(new TestModule(), new DalIoc(),new BusinessIoc());

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

        

        [TestMethod]
        public void TestDBContext()
        {
            var ctx = kernel.Get<IDbContext>();
            ctx.Repository<Article>().DeleteAll().Wait();
            var article = new Article() { Code = "Code", Societe = "999" };
            var entry=ctx.Upsert(article);
            
            Assert.IsTrue(entry.State == EntityState.Added);
            ctx.SaveChangesAsync().Wait();
            Assert.IsTrue(entry.State == EntityState.Unchanged);

            ((Article)entry.Entity).Libelle = "Nouveau libelle";
            Assert.IsTrue(entry.State == EntityState.Modified);

            ctx.SaveChangesAsync().Wait();
            Assert.IsTrue(entry.State == EntityState.Unchanged);

            ctx.Remove(article);
            ctx.SaveChangesAsync().Wait();

            Assert.AreEqual(ctx.Count<Article>(), 0);


            
        }

        [TestMethod]
        public async Task TestDBContext1()
        {
            var ctx = kernel.Get<IDbContext>();
            ctx.Repository<Article>().DeleteAll().Wait();
            var ah = kernel.Get<IArticleBusinessHelper<Article>>();
            var art0 = await ah.Create("Code Article",ArticleType.FraisGeneraux);

            var entry = ctx.Entry(art0);
            Assert.IsNotNull(entry);
            Assert.IsTrue(entry.State == EntityState.Added);
            art0.Save().Wait();

            Assert.IsTrue(entry.State == EntityState.Unchanged);
            Assert.AreEqual(ctx.Count<Article>(), 1);
        }
    }
}
