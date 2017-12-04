using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;
using EastBancTestAssignment.BLL.DTOs;
using EastBancTestAssignment.BLL.Services;
using EastBancTestAssignment.Core.Models;
using EastBancTestAssignment.Web.UI.MVC.ViewModels;

namespace EastBancTestAssignment.Web.UI.MVC.Controllers
{
    public class BackpackController : Controller
    {
        private BackpackTaskService _service;

        public BackpackController()
        {
            _service = new BackpackTaskService(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
//            _service = new BackpackTaskService("DefaultConnection");

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
                Name = "Test Task",
                BackpackWeightLimit = 8,
                Items = new List<ItemViewModel>
                {
                    new ItemViewModel {Name = "Book", Price = 600, Weight = 1},
                    new ItemViewModel {Name = "Binoculars", Price = 5000, Weight = 2},
                    new ItemViewModel {Name = "First Aid Kit", Price = 1500, Weight = 4},
                    new ItemViewModel {Name = "Laptop", Price = 40000, Weight = 2},
                    new ItemViewModel {Name = "Bowler", Price = 500, Weight = 1},
                }
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<ActionResult> NewTask(NewBackpackTaskViewModel vm)
        {
            var itemDtos = new List<ItemDto>();
            foreach (var item in vm.Items)
            {
                itemDtos.Add(new ItemDto
                {
                    Name = item.Name,
                    Price = item.Price,
                    Weight = item.Weight
                });
            }
            BackpackTaskDto backpack = await _service.CreateNewBackpackTask(itemDtos, vm.Name, vm.BackpackWeightLimit);
            await _service.StartBackpackTask(backpack);
           
            return RedirectToAction("NewTask");
        }
    }
}