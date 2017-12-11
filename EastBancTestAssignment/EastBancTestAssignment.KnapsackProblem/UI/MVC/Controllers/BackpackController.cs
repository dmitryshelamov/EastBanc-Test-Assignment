using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Hosting;
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
                
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> NewBackpackTask(BackpackTaskFormViewModel vm)
        {
            var list = Mapper.Map<List<ItemViewModel>, List<ItemDto>>(vm.Items);
            string taskId = await _service.NewBackpackTask(list, vm.Name, vm.BackpackWeightLimit);
            HostingEnvironment.QueueBackgroundWorkItem(ct => _service.StartBackpackTask(taskId, ct));

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