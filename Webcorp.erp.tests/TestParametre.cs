using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Ninject;
using Webcorp.Business;
using Webcorp.Dal;
using Webcorp.Model;
using System.Threading.Tasks;

namespace Webcorp.erp.tests
{
    /// <summary>
    /// Description résumée pour TestParametre
    /// </summary>
    [TestClass]
    public class TestParametre
    {
        public TestParametre()
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
            kernel=Helper.CreateKernel(new TestModule(), new BusinessIoc(), new DalIoc());

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
            Helper.CreateStandardPParametres(true).Wait() ;
            Helper.CreateStandardParametres(true).Wait();

        }
        [TestMethod]
        public async Task TestCreatePParametre1()
        {
            var ph = kernel.Get<IParametreBusinessHelper<Parametre>>();

            var p = await ph.GetParametre(PParametres.RAISONSOCIALE);
         }
    }
}
