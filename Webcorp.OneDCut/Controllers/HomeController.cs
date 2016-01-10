using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webcorp.OneDCut.Models;
using System.Threading;
using System.Threading.Tasks;
using Webcorp.lib.onedcut;
using Ninject;
using Webcorp.Model;
using Webcorp.unite;

namespace Webcorp.OneDCut.Controllers
{
    public class HomeController : AsyncController
    {
    
        public HomeController(IKernel kernel, ISolver solver)
        {
            this.Kernel = kernel;
            this.Solver = solver;
            this.Solver.OnSolved += Solver_OnSolved;
            /*var v = CutModel;
            if (v.IsNull())
            {
                var cutModel = new CutModel();
                CreateTempStock(cutModel);
                CreateTempToCut(cutModel);
                CutModel = cutModel;
            }*/
        }
    
        private CutModel CutModel
        {
            get
            {
                return Session.GetValue<CutModel>("cutmodel",model=> {
                    CreateTempStock(model);
                    CreateTempToCut(model);
                });
            }
            set
            {
                Session.SetValue<CutModel>( value,"cutmodel");
            }
        }
        private void Solver_OnSolved(object sender, SolverEventArgs e)
        {
            AsyncManager.Parameters["args"] = e;
            AsyncManager.OutstandingOperations.Decrement();
        }

        public ISolver Solver { get; private set; }
        public IKernel Kernel { get; private set; }

        public ActionResult Index()
        {

            ViewBag.Title = "OneD Cut";

            return View(CutModel);
        }


        public ActionResult StockDetail(string index)
        {
            return View("StockDetail", CutModel.Stocks.Where(x => x.Id == index).FirstOrDefault());
        }

        public ActionResult ToCutDetail(string index)
        {
            return View("StockDetail", CutModel.ToCut.Where(x => x.Id == index).FirstOrDefault());
        }


        public ActionResult StockEdit(string index)
        {
            ViewBag.Title = "Editer un stock";
            ViewBag.SaveButton = "Sauver le stock";
            return View("StockCreate", CutModel.Stocks.Where(x => x.Id == index).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult StockEdit(Stock stock)
        {
            var stk = CutModel.Stocks;
            var old = stk.Where(x => x.Id == stock.Id).FirstOrDefault();
            if (old != null)
            {
                var idx = stk.IndexOf(old);
                stk.RemoveAt(idx);
                stk.Insert(idx, stock);
                TempData["message"] = "Stock Modifié";
                ClearResult(CutModel);
            }
            else
            {
                TempData["erreur"] = "Impossible de modifer le stock id:" + stock.Id;
            }
            return View("Index", CutModel);
        }

        public ActionResult ToCutEdit(string index)
        {
            ViewBag.Title = "Editer un débit";
            ViewBag.SaveButton = "Sauver le débit";
            return View("StockCreate", CutModel.ToCut.Where(x => x.Id == index).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult ToCutEdit(Stock stock)
        {
            var stk = CutModel.ToCut;
            var old = stk.Where(x => x.Id == stock.Id).FirstOrDefault();
            if (old != null)
            {
                var idx = stk.IndexOf(old);
                stk.RemoveAt(idx);
                stk.Insert(idx, stock);
                TempData["message"] = "Débit Modifié";
                ClearResult(CutModel);
            }
            else
            {
                TempData["erreur"] = "Impossible de modifer le débit id:" + stock.Id;
            }
            return View("Index", CutModel);
        }

        public ActionResult StockCreate()
        {
            ViewBag.Title = "Creer un Stock";
            ViewBag.SaveButton = "Creer le stock";
            return View("StockCreate", new Stock() );
        }

        public ActionResult ToCutCreate()
        {
            ViewBag.Title = "Creer un débit";
            ViewBag.SaveButton = "Creer le débit";
            return View("StockCreate", new Stock() );
        }

        [HttpPost]
        public ActionResult StockCreate(Stock stock)
        {
            if (ModelState.IsValid)
            {
                TempData["message"] = "Stock creer";
                ClearResult(CutModel);
                CutModel.Stocks.Add(stock);
            }
            else
                TempData["erreur"] = string.Format("erreur lors de la creation de {0}", stock.ToString());
            return RedirectToAction("index", CutModel);
            //return View("Index", cutModel);
        }

        [HttpPost]
        public ActionResult ToCutCreate(Stock stock)
        {
            if (ModelState.IsValid)
            {
                TempData["message"] = "Débit creer";
                ClearResult(CutModel);
                CutModel.ToCut.Add(stock);

            }
            else
                TempData["erreur"] = string.Format("erreur lors de la creation de {0}", stock.ToString());
            //return View("Index", cutModel);
            return RedirectToAction("index", CutModel);
        }


        [HttpPost]
        public ActionResult StockDelete(string id)
        {
            CutModel.Stocks.Remove(CutModel.Stocks.Where(x => x.Id == id).FirstOrDefault());
            ClearResult(CutModel);
            TempData["message"] = "Suppresion Ok";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ToCutDelete(string id)
        {
            CutModel.ToCut.Remove(CutModel.ToCut.Where(x => x.Id == id).FirstOrDefault());
            ClearResult(CutModel);
            TempData["message"] = "Suppresion Ok";

            return RedirectToAction("Index");
        }


        public void CalculateAsync()
        {
            AsyncManager.OutstandingOperations.Increment();
            CutModel.Solve = null;
            var _beam = new Article() { Code = "IPE 80", Libelle = "IPE 80", MassLinear = MassLinear.Parse("6 kg/m"), AreaLinear = AreaLinear.Parse("0.328 m2/m"), AreaMass = AreaMass.Parse("54.64 m2/t") };
            _beam.MassCurrency = unite.MassCurrency.Parse("600 euro/tonne");

            Solver.Beams = CutModel.ToBeamToCut(_beam);
            Solver.Stocks = CutModel.ToBeamStock();
            Solver.Beam = _beam;
            Solver.SolveAsync();


        }

        public ActionResult CalculateCompleted(SolverEventArgs args)
        {
           // args.
            CutModel.Solve = args;
            return RedirectToAction("Index",CutModel);
        }


        public ActionResult Clear()
        {
            CutModel = new CutModel();
            return View("Index",CutModel);
        }

        private void ClearResult(CutModel cutmodel)
        {
            CutModel.Solve = null;
        }

        private static void CreateTempStock(CutModel cutModel)
        {
            var result = new List<Stock>();

            result.Add(new Stock() {  Length = 6000, Quantity = 10 });
            cutModel.Stocks = result;
        }

        private static void CreateTempToCut(CutModel cutModel)
        {
            var result = new List<Stock>();
            result.Add(new Stock() {  Length = 4024, Quantity = 2 });
            result.Add(new Stock() {  Length = 1048, Quantity = 5 });
            cutModel.ToCut = result;
        }


    }
}