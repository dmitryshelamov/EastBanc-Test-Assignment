using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using EastBancTestAssignment.BLL.DTOs;
using EastBancTestAssignment.BLL.Services;
using EastBancTestAssignment.Web.UI.MVC.ViewModels;

namespace EastBancTestAssignment.Web.UI.MVC.Controllers
{
    public class BackpackController : Controller
    {
        private BackpackTaskService _service;
        public BackpackController()
        {
            _service = new BackpackTaskService();
        }

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
                    new ItemViewModel { Name = "Book", Price = 600, Weight = 1},
                    new ItemViewModel { Name = "Binoculars", Price = 5000, Weight = 2},
                    new ItemViewModel { Name = "First Aid Kit", Price = 1500, Weight = 4},
                    new ItemViewModel { Name = "Laptop", Price = 40000, Weight = 2},
                    new ItemViewModel { Name = "Bowler", Price = 500, Weight = 1},

                    new ItemViewModel { Name = "Lighter", Price = 400, Weight = 1},
                    new ItemViewModel { Name = "Tent", Price = 10000, Weight = 2},
                    new ItemViewModel { Name = "Radio", Price = 2000, Weight = 1},
                }
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<ActionResult> NewTask(NewBackpackTaskViewModel vm)
        {
            List<ItemDto> itemDtos = vm.Items.Select(itemDto => new ItemDto
            {
                Name = itemDto.Name,
                Price = itemDto.Price,
                Weight = itemDto.Weight
            }).ToList();
            var bt = await _service.CreateNewBackpackTask(itemDtos, vm.Name, vm.BackpackWeightLimit);
            await _service.StartBackpackTask(bt);
            return RedirectToAction("Index");
        }
    }
}