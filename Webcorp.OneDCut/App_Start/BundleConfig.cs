using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Transformers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Webcorp.OneDCut
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif
            #region Less Css Transformation
            var cssTransformer = new StyleTransformer();
            var jsTransformer = new ScriptTransformer();
            var nullOrderer = new NullOrderer();

            var css = new StyleBundle("~/bundles/css")
            .Include("~/Content/site.less")
            .Include("~/Content/jquery.bootstrap-touchspin.css");
            css.Transforms.Add(cssTransformer);
            css.Orderer = nullOrderer;
            #endregion

            #region JQuery
            
            var jquery = new ScriptBundle("~/bundles/jquery");
            
            var sdir = "~/Scripts";
#if DEBUG

            var jqueries = new string[] { "jquery-1.9.0", "jquery.validate", "jquery.validate.unobtrusive", "jquery.validate.unobtrusive.bootstrap" ,"jquery.bootstrap-touchspin","bootstrap-toolkit"};
            
#endif
            jquery.Include(sdir, jqueries);
            #endregion

            #region site
            var site = new ScriptBundle("~/bundles/site");
            site.Include("~/js/site.js");
            #endregion

            bundles.Add(css);
            bundles.Add(jquery);
            bundles.Add(site);
        }
    }
}