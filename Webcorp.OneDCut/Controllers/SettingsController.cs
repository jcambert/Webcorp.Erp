using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webcorp.lib.onedcut;
using Webcorp.OneDCut.Models;

namespace Webcorp.OneDCut.Controllers
{
    public class SettingsController : Controller
    {
        IKernel kernel;
        public SettingsController(IKernel kernel)
        {
            this.kernel = kernel;
           
            
        }

        [HttpPost]
        public ActionResult Index(SettingsModel settings)
        {
            //settings.OrignalParameters=
            var param = kernel.Get<ISolverParameter>();
            param.CrossoverProbability = settings.CrossoverProbability;
            param.ElitePercentage = settings.ElitePercentage;
            param.InitialPopulationCount = settings.InitialPopulationCount;
            param.MaxEvaluation = settings.MaxEvaluation;
            param.MutationProbability = settings.MutationProbability;
           // param.Save();
            //return RedirectToAction("index","home");
            return View("index",settings);
        }
        // GET: Settings
        public ActionResult Index()

        {
            var settings = kernel.Get<SettingsModel>();
            return View(settings);
        }
    }
}