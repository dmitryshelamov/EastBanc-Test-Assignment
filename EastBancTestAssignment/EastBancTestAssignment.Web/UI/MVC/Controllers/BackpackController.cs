using System.Collections.Generic;
using System.Web.Mvc;
using EastBancTestAssignment.Web.UI.MVC.ViewModels;

namespace EastBancTestAssignment.Web.UI.MVC.Controllers
{
    public class BackpackController : Controller
    {
        // GET: Backpack
        public ActionResult Index()
        {
            List<BackpackTaskViewModel> list = new List<BackpackTaskViewModel>();
            return View(list);
        }
    }
}