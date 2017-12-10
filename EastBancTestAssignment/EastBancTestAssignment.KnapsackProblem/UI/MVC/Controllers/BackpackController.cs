using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using EastBancTestAssignment.KnapsackProblem.BLL.DTOs;
using EastBancTestAssignment.KnapsackProblem.BLL.Services;
using EastBancTestAssignment.KnapsackProblem.UI.MVC.ViewModels;

namespace EastBancTestAssignment.KnapsackProblem.UI.MVC.Controllers
{
    [OutputCache(NoStore = true, Duration = 0)]
    public class BackpackController : Controller
    {
        private BackpackTaskService _service;

        public BackpackController()
        {
            _service = new BackpackTaskService();
        }
        // GET: Backpack
        public async Task<ActionResult> Index()
        {
            List<BackpackTaskDto> taskDtos = await _service.GetAllBackpackTaskWithRelatedObjectsAsync();
            List<BackpackTaskViewModel> list = Mapper.Map<List<BackpackTaskDto>, List<BackpackTaskViewModel>>(taskDtos);
            return View(list);
        }

        public ActionResult NewBackpackTask()
        {
            var viewModel = new BackpackTaskFormViewModel
            {
                Name = "Task Name",
                BackpackWeightLimit = 8,
                Items = new List<ItemViewModel>
                {
                    new ItemViewModel {Name = "Book", Price = 600, Weight = 1},
                    new ItemViewModel {Name = "Binoculars", Price = 5000, Weight = 2},
                    new ItemViewModel {Name = "First Aid Kit", Price = 1500, Weight = 4},
                    new ItemViewModel {Name = "Laptop", Price = 40000, Weight = 2},
                    new ItemViewModel {Name = "Bowler", Price = 500, Weight = 1},

                    new ItemViewModel { Name = "Item 1", Price = 100, Weight = 1},
                    new ItemViewModel { Name = "Item 2", Price = 200, Weight = 1},
                    new ItemViewModel { Name = "Item 3", Price = 300, Weight = 3},
                    new ItemViewModel { Name = "Item 4", Price = 400, Weight = 2},
                    new ItemViewModel { Name = "Item 5", Price = 500, Weight = 1},
                }
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> NewBackpackTask(BackpackTaskFormViewModel vm)
        {
            var list = Mapper.Map<List<ItemViewModel>, List<ItemDto>>(vm.Items);
            string taskId = await _service.NewBackpackTask(list, vm.Name, vm.BackpackWeightLimit);

            CancellationTokenSource cancellation = new CancellationTokenSource();
            CancellationToken token = cancellation.Token;
            BackpackTaskService.CancellationTokens.Add(taskId, cancellation);

            Task.Run(() => _service.StartBackpackTask(taskId, token));
            return RedirectToAction("Index");
        }

        public ActionResult Details(string id)
        {
            BackpackTaskDto backpackTaskDto = _service.GetBackpackTask(id);
            BackpackTaskDetailViewModel vm = Mapper.Map<BackpackTaskDto, BackpackTaskDetailViewModel>(backpackTaskDto);
            return View(vm);
        }
    }
}