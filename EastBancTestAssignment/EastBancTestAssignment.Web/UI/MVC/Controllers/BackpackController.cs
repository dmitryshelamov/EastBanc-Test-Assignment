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
        public const string InProgress = "In Progress";
        public const string Complete = "Complete";
        public BackpackController()
        {
            _service = BackpackTaskService.GetInstance();
        }

        // GET: Backpack
        public async Task<ActionResult> Index()
        {
            var taskDtos = await _service.GetAllBackpackTasks();
            List<BackpackTaskViewModel> list = new List<BackpackTaskViewModel>();
            foreach (var backpackTask in taskDtos)
            {
                

                list.Add(new BackpackTaskViewModel
                {
                    Id = backpackTask.Id,
                    Name = backpackTask.Name,
                    BackpackWeightLimit = backpackTask.WeightLimit,
                    BestPrice = backpackTask.BestItemSetPrice,
                    PercentComplete = GetPercent(backpackTask),
                    Status = GetStatus(backpackTask)
                });
            }

            return View(list);
        }

        public ActionResult NewTask()
        {
            var vm = new NewBackpackTaskViewModel
            {
                Name = "Test Name",
                BackpackWeightLimit = 8,
                Items = new List<ItemViewModel>
                {
                    new ItemViewModel { Name = "Book", Price = 600, Weight = 1},
                    new ItemViewModel { Name = "Binoculars", Price = 5000, Weight = 2},
                    new ItemViewModel { Name = "First Aid Kit", Price = 1500, Weight = 4},
                    new ItemViewModel { Name = "Laptop", Price = 40000, Weight = 2},
                    new ItemViewModel { Name = "Bowler", Price = 500, Weight = 1},

                    new ItemViewModel { Name = "Item 1", Price = 100, Weight = 1},
                    new ItemViewModel { Name = "Item 2", Price = 200, Weight = 1},
                    new ItemViewModel { Name = "Item 3", Price = 300, Weight = 3},
                    new ItemViewModel { Name = "Item 4", Price = 400, Weight = 2},
                    new ItemViewModel { Name = "Item 5", Price = 500, Weight = 1},


                    new ItemViewModel { Name = "Item 6", Price = 600, Weight = 4},
                    new ItemViewModel { Name = "Item 7", Price = 700, Weight = 3},
                    new ItemViewModel { Name = "Item 8", Price = 800, Weight = 3},
                    new ItemViewModel { Name = "Item 9", Price = 900, Weight = 2},
                    new ItemViewModel { Name = "Item 10", Price = 100, Weight = 4},

//                    new ItemViewModel { Name = "Lighter", Price = 400, Weight = 1},
//                    new ItemViewModel { Name = "Tent", Price = 10000, Weight = 2},
//                    new ItemViewModel { Name = "Radio", Price = 2000, Weight = 1},
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
            Task.Run(() => _service.StartBackpackTask(bt));
//            await _service.StartBackpackTask(bt);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(string id)
        {
            var backpackTaskDto = await _service.GetBackpackTask(id);

            var vm = new BackpackTaskSolutionViewModel();

            vm.Id = backpackTaskDto.Id;
            vm.Name = backpackTaskDto.Name;
            vm.WeightLimit = backpackTaskDto.WeightLimit;
            vm.Status = GetStatus(backpackTaskDto);
            vm.Items = backpackTaskDto.ItemDtos.Select(item => new ItemViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                Weight = item.Weight
            }).ToList();

            if (vm.Status == Complete)
            {
                vm.BestItemSetWeight = backpackTaskDto.BestItemSetWeight;
                vm.BestPrice = backpackTaskDto.BestItemSetPrice;
                vm.BestItemSet = backpackTaskDto.BestItemDtosSet.Select(item => new ItemViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    Weight = item.Weight
                }).ToList();
                vm.CalculationTime = backpackTaskDto.EndTime - backpackTaskDto.StartTime;
            }

            

            return View(vm);
        }

        public async Task<ActionResult> Delete(string id)
        {
            await _service.Delete(id);
            return RedirectToAction("Index");
        }

        private string GetStatus(BackpackTaskDto backpackTask)
        {
            var status = InProgress;
            if (GetPercent(backpackTask) == 100)
                status = Complete;
            return status;
        }

        private int GetPercent(BackpackTaskDto backpackTask)
        {
            return (int)(backpackTask.CombinationCalculated / backpackTask.ItemCombinationDtos.Count) * 100;
        }

    }
}