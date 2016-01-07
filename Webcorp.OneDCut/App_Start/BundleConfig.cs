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
            var cssTransformer = new StyleTransformer();
            var jsTransformer = new ScriptTransformer();
            var nullOrderer = new NullOrderer();

            var css = new StyleBundle("~/bundles/css")
            .Include("~/Content/site.less");
            css.Transforms.Add(cssTransformer);
            css.Orderer = nullOrderer;
            

#if DEBUG
            BundleTable.EnableOptimizations = true;
#endif

            bundles.Add(css);


        }
    }
}