using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using EastBancTestAssignment.Web.App_Start;
using EastBancTestAssignment.Web.UI.MVC;

namespace EastBancTestAssignment.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new CustomViewEngine());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
