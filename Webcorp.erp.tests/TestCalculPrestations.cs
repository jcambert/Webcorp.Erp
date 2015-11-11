using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Diagnostics;
using Webcorp.Model.Quotation;
using Webcorp.unite;

namespace Webcorp.erp.tests
{
    /// <summary>
    /// Description résumée pour TestCalculPrestations
    /// </summary>
    [TestClass]
    public class TestCalculPrestations
    {
        public TestCalculPrestations()
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

        static IKernel container;



        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            Debug.WriteLine("ClassInit " + context.TestName);
            container = new StandardKernel(new TestModule());

        }

        [TestMethod]
        public void TestSimpleCalculMassGeneric()
        {
            CalculPrestation<Mass, MassCurrency> cpm = new CalculPrestation<Mass, MassCurrency>();
            ICalculTranche<Mass, MassCurrency> ict = new CalculTranche<Mass, MassCurrency>();
            ict.Add(11.4 * Mass.Kilogram, MassCurrency.EuroKilogramme * 55, true);
            ict.Add(25 * Mass.Kilogram, MassCurrency.EuroKilogramme * 4.83);
            ict.Add(50 * Mass.Kilogram, MassCurrency.EuroKilogramme * 3.87);
            ict.Add(100 * Mass.Kilogram, MassCurrency.EuroKilogramme * 3.38);
            ict.Add(200 * Mass.Kilogram, MassCurrency.EuroKilogramme * 3.14);
            ict.Add(9999 * Mass.Kilogram, MassCurrency.EuroKilogramme * 1.05);

            var result = cpm.Calculate(Mass.Kilogram * 10, ict);
            Assert.AreEqual(result, 55 * Currency.Euro);

            result = cpm.Calculate(Mass.Kilogram * 27, ict);
            Assert.AreEqual(result, 3.87 * 27 * Currency.Euro);

            result = cpm.Calculate(Mass.Kilogram * 250, ict);
            Assert.AreEqual(result, 1.05 * 250 * Currency.Euro);
        }


        [TestMethod]
        public void TestSimpleCalculAreaGeneric()
        {
            CalculPrestation<Area, AreaCurrency> cpm = new CalculPrestation<Area, AreaCurrency>();
            ICalculTranche<Area, AreaCurrency> ict = new CalculTranche<Area, AreaCurrency>();
            ict.Add(AreaCurrency.EuroSquareMetre * 15, Currency.Euro * 80);


            var result = cpm.Calculate(Area.SquareMetre * 10, ict);
            Assert.AreEqual(result, 150 * Currency.Euro);

            result = cpm.Calculate(Area.SquareMetre * 1.5, ict);
            Assert.AreEqual(result, 80 * Currency.Euro);
        }
    }
}
