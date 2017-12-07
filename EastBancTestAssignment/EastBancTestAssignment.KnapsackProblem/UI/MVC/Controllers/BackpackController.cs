using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EastBancTestAssignment.KnapsackProblem.BLL.DTOs;
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

        public ActionResult NewBackpackTask()
        {
            var viewModel = new BackpackTaskFormViewModel
            {
                Items = new List<ItemViewModel>
                {
                    new ItemViewModel {Name = "Book", Price = 600, Weight = 1},
                    new ItemViewModel {Name = "Binoculars", Price = 5000, Weight = 2},
                    new ItemViewModel {Name = "First Aid Kit", Price = 1500, Weight = 4},
                    new ItemViewModel {Name = "Laptop", Price = 40000, Weight = 2},
                    new ItemViewModel {Name = "Bowler", Price = 500, Weight = 1},
                }
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult NewBackpackTask(BackpackTaskFormViewModel vm)
        {
            return RedirectToAction("Index");
        }
    }
}