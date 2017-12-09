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

            bundles.Add(new ScriptBundle("~/bundles/signalr").Include(
                "~/UI/Scripts/jquery.signalR-{version}.js",
                "~/UI/Scripts/Custom/SignalRClient.js"));

            bundles.Add(new ScriptBundle("~/bundles/newtask").Include(
                "~/UI/Scripts/Custom/AddNewRow.js"));

            bundles.Add(new ScriptBundle("~/bundles/removerow").Include(
                "~/UI/Scripts/Custom/DeleteRow.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/UI/Content/css/bootstrap.css",
                "~/UI/Content/css/site.css"));
        }
    }
}