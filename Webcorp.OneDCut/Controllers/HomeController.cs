using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webcorp.OneDCut.Models;

namespace Webcorp.OneDCut.Controllers
{
    public class HomeController : Controller
    {
        static CutModel cutModel;
        static HomeController()
        {
            cutModel = new CutModel();
            CreateTempStock(cutModel);
            CreateTempToCut(cutModel);
        }
        public HomeController()
        {

        }
        public ActionResult Index()
        {

            ViewBag.Title = "OneD Cut";

            return View(cutModel);
        }


        public ActionResult StockDetail(int index)
        {
            return View("StockDetail", cutModel.Stocks.Where(x => x.Id == index).FirstOrDefault());
        }

        public ActionResult ToCutDetail(int index)
        {
            return View("StockDetail", cutModel.ToCut.Where(x => x.Id == index).FirstOrDefault());
        }


        public ActionResult StockCreate()
        {
            return View("StockCreate", new Stock());
        }

        public ActionResult ToCutCreate()
        {
            return View("StockCreate", new Stock());
        }

        [HttpPost]
        public ActionResult StockCreate(Stock stock)
        {
            if (ModelState.IsValid)
            {
                TempData["message"] = string.Format("{0} creer", stock.ToString());
                cutModel.Stocks.Add(stock);
            }
            else
                TempData["erreur"] = string.Format("erreur lors de la creation de {0}", stock.ToString());
            return View("Index", cutModel);
        }

        [HttpPost]
        public ActionResult ToCutCreate(Stock stock)
        {
            if (ModelState.IsValid)
            {
                TempData["message"] = string.Format("{0} creer", stock.ToString());
                cutModel.ToCut.Add(stock);
            }
            else
                TempData["erreur"] = string.Format("erreur lors de la creation de {0}", stock.ToString());
            return View("Index", cutModel);
        }


        [HttpPost]
        public ActionResult StockDelete(int id)
        {
            cutModel.Stocks.Remove(cutModel.Stocks.Where(x => x.Id == id).FirstOrDefault());
            
                TempData["message"] = "Suppresion Ok";
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ToCutDelete(int id)
        {
            cutModel.ToCut.Remove(cutModel.ToCut.Where(x => x.Id == id).FirstOrDefault());

            TempData["message"] = "Suppresion Ok";

            return RedirectToAction("Index");
        }

        private static void CreateTempStock(CutModel cutModel)
        {
            var result = new List<Stock>();

            result.Add(new Stock() { Id = 1, Length = 6000, Quantity = 10 });
            cutModel.Stocks = result;
        }

        private static void CreateTempToCut(CutModel cutModel)
        {
            var result = new List<Stock>();
            result.Add(new Stock() { Id = 1, Length = 4024, Quantity = 2 });
            result.Add(new Stock() { Id = 2, Length = 1048, Quantity = 5 });
            cutModel.ToCut = result;
        }


    }
}