using System.Web.Optimization;

namespace EastBancTestAssignment.KnapsackProblem.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/UI/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/UI/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/UI/Content/css/bootstrap.css",
                "~/UI/Content/css/site.css"));
        }
    }
}