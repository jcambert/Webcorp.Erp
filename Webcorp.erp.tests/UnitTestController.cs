using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Ninject;
using System.Reflection;
using Webcorp.Controller;
using Webcorp.Model;
using System.Threading.Tasks;
using System.Linq;
using Webcorp.Dal;
using Webcorp.unite;
using Webcorp.Business;
using Webcorp.Model.Quotation;
using PropertyChanged;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace Webcorp.erp.tests
{
    [TestClass]
    public class UnitTestController
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
            container = new StandardKernel(new TestModule(), new DalIoc());
            var bap= container.Get<IBusinessAssemblyProvider>();
            bap.Assemblies.Add(Assembly.GetAssembly(typeof(AbstractBusinessController<>)));
            //container.Load(Assembly.GetExecutingAssembly());
        }

       /* [TestInitialize()]
        public void  Initialize()
        {
            Debug.WriteLine("TestMethodInit");
        }*/
        [TestMethod]
        public async Task TestEntityController()
        {
            try
            {
                var mp = container.Get<IEntityProvider<Material,string>>();
                var ctrl = container.Get<IEntityController<Material>>();
                Assert.IsNotNull(ctrl);
                await ctrl.Repository.DeleteAll();
                var material = mp.Find("1.0035");
                Assert.IsNotNull(material);
                var result0=await ctrl.Post(material);
                result0.Throw();


                var result = await ctrl.Get(material.Id);
                Assert.IsNotNull(result);
                Assert.AreEqual(material.Number, result.Number);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public  async Task TestBusinessEntityController()
        {
            container.Unbind(typeof(IEntityController<>));
            container.Bind(typeof(IEntityController<>)).To(typeof(BusinessEntityController<>));

            try
            {
                var mp = container.Get<IEntityProvider<Material, string>>();
                var mpp = container.Get<IEntityProvider<MaterialPrice, string>>();
                var ctrl = container.Get<IEntityController<Material>>();
                Assert.IsNotNull(ctrl);
                await ctrl.Repository.DeleteAll();
                var material = mp.Find("1.0035");
                Assert.IsNotNull(material);
                var result0=await ctrl.Post(material);
                result0.Throw();


                var result = await ctrl.Get(material.Id);
                Assert.IsNotNull(result);
                Assert.AreEqual(material.Number, result.Number);

                

                var mp0 = mpp.Find("1.0035");
                var ctrl1= container.Get<IEntityController<MaterialPrice>>();
                await ctrl1.Post(mp0);

                var mp1 = await ctrl1.Get(mp0.Id);
                Assert.AreEqual(mp0.Cout, mp1.Cout);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestBusinessController()
        {
            container.Unbind(typeof(IEntityController<>));
            container.Bind(typeof(IEntityController<>)).To(typeof(BusinessEntityController<>));
            var busprov = container.Get<IBusinessControllerProvider<Material>>();
            var bap = container.Get<IBusinessAssemblyProvider>();
            Assert.IsNotNull(busprov);
            Assert.IsNotNull(bap);
            Assert.AreEqual(bap.BusinessControllers<Material>().Count, busprov.Controllers.ToList().Count);
            Debug.WriteLine("Material Business controller count:" + busprov.Controllers.ToList().Count);
        }

        [TestMethod]
        public async Task TestCurrencySerializer()
        {
            var mpp = container.Get<IEntityProvider<MaterialPrice, string>>();
            var mp = mpp.Find("1.0035");
            var ctrl1 = container.Get<IEntityController<MaterialPrice>>();
            var result =await ctrl1.Post(mp);
            result.Throw();
            var mp1 = await ctrl1.Get(mp.Id);
            Assert.AreEqual(mp.Cout, mp1.Cout);
        }

        [TestMethod]
        public async Task TestQuotation()
        {
            var q = new Quotation() { Numero = 1234 };
            var ctrl=container.Get<IEntityController<Quotation>>();
            var result = await ctrl.Post(q);
            result.Throw();
            var q1 = await ctrl.Get(q.Id);
            Assert.AreEqual(q.Numero, q1.Numero);
        }

        [TestMethod]
        public async Task TestQuotationWithEntities()
        {
            var pcp = container.Get<IEntityProvider<PosteCharge, int>>();
            var q = new Quotation() { Numero = 1234 };
            var e = new EntityQuotation(q) { Reference = "reftest", Designation = "refdesi", };
            var op1 = new Operation(e) {Poste=pcp.Find(300),Nombre=2 };
            Debug.WriteLine(op1);
            var op2 = new Operation(e) { Poste =pcp.Find(101), Nombre = 5 };
            Debug.WriteLine(op2);

            new Tarif(e) { Quantite = 1,CoeficientVente=0.4 };
            new Tarif(e) { Quantite = 5, CoeficientVente = 0.35 };
            new Tarif(e) { Quantite = 10, CoeficientVente = 0.30 };
            new Tarif(e) { Quantite = 20, CoeficientVente = 0.20};
            new Tarif(e) { Quantite = 50, CoeficientVente = 0.20};
            new Tarif(e) { Quantite = 100, CoeficientVente = 0.20 };
            Debug.WriteLine(e);

            var ctrl = container.Get<IEntityController<Quotation>>();
            var result = await ctrl.Post(q);
            result.Throw();
            var q1 = await ctrl.Get(q.Id);
            Assert.AreEqual(q.Numero, q1.Numero);
        }


        [TestMethod]
        public void TestVitesseDecoupeLaserProvider()
        {
            var props = typeof(VitesseDecoupeLaser).GetPropertiesSortedByFieldOrder<KeyProviderAttribute>();
            Assert.AreEqual(props.Count, 4);
            var pcp=container.Get<IEntityProvider<VitesseDecoupeLaser, string>>();
            var vitesse=pcp.Find(215,MaterialGroup.P,1,GazDecoupe.Oxygene);
            Assert.IsNotNull(vitesse);
            pcp.Keys.ForEach(p => Debug.WriteLine("Vitesse Key:"+p));
        }
        [TestMethod]
        public async Task TestQuotationWithEntitiesAndDecoupeLaser()
        {
      
            var pcp = container.Get<IEntityProvider<PosteCharge, int>>();
            var vdlp= container.Get<IEntityProvider<VitesseDecoupeLaser, string>>();
            var mpp = container.Get<IEntityProvider<MaterialPrice, string>>();
            var mp = container.Get<IEntityProvider<Material, string>>();
            var q = new Quotation() { Numero = 1234 };
           
            
            var e = new EntityQuotation(q) { Reference = "reftest", Designation = "refdesi" };
            e.Outillage = 50 * Currency.Euro;
           
            var op1 = new Operation(e) { Poste = pcp.Find(300), Nombre = 2 };
            Debug.WriteLine(op1);
            var op2 = new Operation(e) { Poste = pcp.Find(101), Nombre = 5 };
            Debug.WriteLine(op2);

            var op3 = new Operation(e) { Poste = pcp.Find(215), Nombre = 1 };
            var decoupe = new OperationLaser(op3) { Epaisseur = 1, Gaz = GazDecoupe.Oxygene, Longueur = 600 * Length.Millimetre, NombreAmorcage = 3, NombrePetitDiametre = 2 ,SqueletteX=20*Length.Millimetre,SqueletteY=20*Length.Millimetre,Pince=0*Length.Millimetre};
            decoupe.FormatPiece = new Format() { Longueur = 200 * Length.Millimetre, Largeur = 100 * Length.Millimetre, Epaisseur = 1 * Length.Millimetre, Matiere = mp.Find("1.0035") };
            decoupe.FormatTole = new Format() { Longueur = 222  * Length.Millimetre, Largeur = 125 * Length.Millimetre, Epaisseur = 1 * Length.Millimetre, Matiere = mp.Find("1.0035"),PrixMatiere=mpp.Find("1.0035") };

            decoupe.Laser = vdlp.Find(215, MaterialGroup.P, 1, GazDecoupe.Oxygene);
            
            Debug.WriteLine(op3);

            

            Tarif tarif1=new Tarif(e) { Quantite = 1, CoeficientVente = 0.4 };
            tarif1.Prestations.Add(new TarifPrestation() {Estime=true,Prestation="Epoxy",Tarif=Currency.Euro*80 });
            new Tarif(e) { Quantite = 5, CoeficientVente = 0.35 };
            new Tarif(e) { Quantite = 10, CoeficientVente = 0.30 };
            new Tarif(e) { Quantite = 20, CoeficientVente = 0.20 };
            new Tarif(e) { Quantite = 50, CoeficientVente = 0.20 };
            new Tarif(e) { Quantite = 100, CoeficientVente = 0.20 };
            Debug.WriteLine(e);

            var ctrl = container.Get<IEntityController<Quotation>>();
            var result = await ctrl.Post(q);
            result.Throw();
            var q1 = await ctrl.Get(q.Id);
            Assert.AreEqual(q.Numero, q1.Numero);

            Debug.WriteLine(q1.Entities[0]);
        }

        [TestMethod]
        public void TestPoidsEtPrixMatiere()
        {
            var mpp = container.Get<IEntityProvider<MaterialPrice, string>>();
            var mp = container.Get<IEntityProvider<Material, string>>();

            var format= new Format() { Longueur = 222 * Length.Millimetre, Largeur = 125 * Length.Millimetre, Epaisseur = 1 * Length.Millimetre, Matiere = mp.Find("1.0035"), PrixMatiere = mpp.Find("1.0035") };

            Assert.AreEqual(format.Poids.ConvertTo(Mass.Kilogram), (222 * 125 * 1 * 7.8)/1000000);
            Debug.WriteLine(format.Poids.ToString("0.00 [kg]"));

            Debug.WriteLine(mpp.Find("1.0035").Cout.ToString("0.00 [euro/kg]"));

            Debug.WriteLine(format.CoutMatiere.ToString("0.00 [euro]"));
        }

        [TestMethod]
        public void TestPropertyChange()
        {
            Quotation q = new Quotation();
            /*q.PropertyChanged += (sender,args)=> {
                Debug.WriteLine("Property Changed:" + args.PropertyName);
            };
            q.PropertyChanging += (send, args) =>
            {
                Debug.WriteLine("Property Changing:" + args.PropertyName);
            };*/
            q.Numero = 1234;
            q.IsSelected = true;

            var tmp = new Tmp();
            tmp.PropertyChanged += (sender, args) =>
            {
                Debug.WriteLine("Property Changed:" + args.PropertyName);
            };
            tmp.Numero = 1;

        }

        [TestMethod]
        public void TestCurrencyFormat()
        {
            Currency c = (1 / 3) * Currency.Euro;
            Debug.WriteLine( c.ToString("0.00"));
        }
        
    }
   
   
    public class Tmp:ReactiveUI.ReactiveObject
    {
        public new event PropertyChangedEventHandler PropertyChanged;
        public Tmp()
        {
            PropertyChanged += Tmp_PropertyChanged;
        }

        private void Tmp_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
           
        }

        public int Numero { get; set; }

        public void OnPropertyChanged([CallerMemberName] string sender = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(sender));
        }

    
    }
}
