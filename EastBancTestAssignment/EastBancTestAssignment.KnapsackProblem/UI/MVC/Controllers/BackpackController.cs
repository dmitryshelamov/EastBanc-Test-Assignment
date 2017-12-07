using System.Collections.Generic;
using System.Web.Mvc;
using EastBancTestAssignment.KnapsackProblem.UI.MVC.ViewModels;

namespace EastBancTestAssignment.KnapsackProblem.UI.MVC.Controllers
{
    public class BackpackController : Controller
    {
        // GET: Backpack
        public ActionResult Index()
        {
            return View(new List<BackpackTaskViewModel>());
        }
    }
}