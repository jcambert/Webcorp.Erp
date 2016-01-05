using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Diagnostics;
using Webcorp.Business;
using Webcorp.Dal;
using Webcorp.Model;
using System.Threading.Tasks;

namespace Webcorp.erp.tests
{
    [TestClass]
    public class TestUtilisateur
    {

        static IKernel kernel;



        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            Debug.WriteLine("ClassInit " + context.TestName);
            kernel = Helper.CreateKernel(new TestModule(), new BusinessIoc(), new DalIoc());
        }
        [TestMethod]
        public async Task TestCreateSimpleUser()
        {
            var auth = kernel.Get<IAuthenticationService>();
            var uh = kernel.Get<IUtilisateurBusinessHelper<Utilisateur>>();
            uh.DeleteAll().Wait();

            var uti=await uh.Create("999", "jcambert", "korben90", "Ambert", "Jean-Christophe", "jc.ambert@gmail.com");
            uh.Save().Wait();
            
        }
    }
}
