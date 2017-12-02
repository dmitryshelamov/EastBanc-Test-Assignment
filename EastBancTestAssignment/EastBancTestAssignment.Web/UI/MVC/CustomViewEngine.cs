using System.Web.Mvc;

namespace EastBancTestAssignment.Web.UI.MVC
{
    public class CustomViewEngine : RazorViewEngine
    {
        public CustomViewEngine()
        {
            ViewLocationFormats = new[]
            {
                "~/UI/MVC/Views/{1}/{0}.cshtml",
                "~/UI/MVC/Views/Shared/{1}/{0}.cshtml"
            };

            MasterLocationFormats = ViewLocationFormats;
            PartialViewLocationFormats = ViewLocationFormats;
        }
    }
}