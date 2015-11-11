using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Webcorp.Model;
using Webcorp.Model.Quotation;
using Ninject;

namespace Webcorp.unite.tests
{
    [TestClass]
    public class UnitTest1
    {
        static IKernel container;

        [AssemblyInitialize()]
        public static void AssemblyInit(TestContext context)
        {
            Debug.WriteLine("AssemblyInit " + context.TestName);
        }

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            Debug.WriteLine("ClassInit " + context.TestName);
            container = new StandardKernel();
            container.Bind(typeof(IEntityProvider<,>)).To(typeof(EntityProvider<,>)).InSingletonScope();
            container.Bind(typeof(IEntityProviderInitializable<MaterialPrice, string>)).To(typeof(MaterialPriceInitializer));
            
        }

        [TestMethod]
        public void TestMethod1()
        {
            var l = 1 * Length.Mile;
            Debug.WriteLine(l.ToString("0.00 [km]"));
            Debug.WriteLine(l.ToString("0.00 [mi]"));
            var l1 = l.ConvertTo(Length.Mile);
            Debug.WriteLine(l1.ToString());

            var l2 = Length.Parse(l.ToString("0.00 [km]"));
            Debug.WriteLine(l2.ToString());
            // Debug.WriteLine(UnitProvider.Default.GetDisplayUnit(l1.GetType()));
        }

        [TestMethod]
        public void TestCurrency()
        {
            /*var v = 1 * Currency.Euro;
            Debug.WriteLine(v.ToString());
            Debug.WriteLine(v.ToString("0.00 [chf]"));

            var v2 = 1 * Currency.FrancSuisse;
            Debug.WriteLine(v2.ToString("0.00 [chf]"));
            Debug.WriteLine(v2.ToString("0.00 [euro]"));*/

            Currency.FrancSuisse.SetChange(2);
            var v3 = 1 * Currency.Euro;
            Debug.WriteLine(v3.ToString());
            Debug.WriteLine(v3.ToString("0.00 [chf]"));

            var v4 = 1 * Currency.FrancSuisse;
            Debug.WriteLine(v4.ToString("0.00 [chf]"));
            Debug.WriteLine(v4.ToString("0.00 [euro]"));

        }

        [TestMethod]
        public void TestMaterialPrice()
        {
            var mpp = container.Get<IEntityProvider<MaterialPrice, string>>();
            MaterialPrice mp = mpp.Find("1.0035");
            Assert.IsNotNull(mp);
            
            Debug.WriteLine(mp.Cout.ToString("0.00 euro/kg"));

            MaterialPrice mp1 = mpp.Find("1.0036");
            Debug.WriteLine( mp1.Cout.ToString("0.00 euro/kg"));
        }

       
    }
}
