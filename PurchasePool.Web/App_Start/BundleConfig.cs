using System.Web;
using System.Web.Optimization;

namespace PurchasePool.Web.App_Start
{
    public class BundleConfig
    {
        public static BundleCollection RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/scripts/lib/angular")
                .Include("~/Scripts/angular/angular.js"));

            bundles.Add(new ScriptBundle("~/scripts/lib/ui-router")
               .Include("~/Scripts/ui-router/*.js"));

            bundles.Add(new ScriptBundle("~/scripts/lib/bootstrap")
                .Include("~/Scripts/bootstrap/*.js"));

            bundles.Add(new StyleBundle("~/css/lib/bootstrap")
                .Include("~/Content/bootstrap/*.css"));

            bundles.Add(new ScriptBundle("~/scripts/app")
                .Include("~/Scripts/app/*.js"
                ,"~/Scripts/app/Controllers/*.js"
                ,"~/Scripts/app/Providers/*.js"
                , "~/Scripts/app/Config/*.js"));

            return bundles;
        }
    }
}