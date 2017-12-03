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

        public ActionResult NewTask()
        {
            var vm = new NewBackpackTaskViewModel
            {
                Items = new List<ItemViewModel>
                {
                    new ItemViewModel { Name = "CustomName#1", Price = 1, Weight = 10 },
                    new ItemViewModel { Name = "CustomName#2", Price = 2, Weight = 20 },
                    new ItemViewModel { Name = "CustomName#3", Price = 3, Weight = 30 },
                }
            };
            return View(vm);
        }

        [HttpPost]
        public ActionResult NewTask(NewBackpackTaskViewModel vm)
        {
            var v = vm;
            return RedirectToAction("NewTask");
        }
    }
}