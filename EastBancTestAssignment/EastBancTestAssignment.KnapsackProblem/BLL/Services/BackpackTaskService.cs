using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Hosting;
using AutoMapper;
using EastBancTestAssignment.KnapsackProblem.BLL.DTOs;
using EastBancTestAssignment.KnapsackProblem.DAL;
using EastBancTestAssignment.KnapsackProblem.DAL.Models;

namespace EastBancTestAssignment.KnapsackProblem.BLL.Services
{
    public class BackpackTaskService
    {

        public async Task<string> NewBackpackTask(List<ItemDto> itemDtos, string taskName,
            int backpackWeightLimit)
        {
            BackpackTask backpackTask = new BackpackTask
            {
                Name = taskName,
                BackpackItems = Mapper.Map<List<ItemDto>, List<Item>>(itemDtos),
                WeightLimit = backpackWeightLimit,
                StartTime = DateTime.Now
            };

            var unitOfWork = UnitOfWork.UnitOfWorkFactory();
            unitOfWork.BackpackTaskRepository.Add(backpackTask);
            await unitOfWork.CompleteAsync();

            return backpackTask.Id;
        }

        public async Task StartBackpackTask(string id, CancellationToken token)
        {
            var unitOfWork = UnitOfWork.UnitOfWorkFactory();
            BackpackTask backpackTask = unitOfWork.BackpackTaskRepository.Get(id);
            TaskProgress taskProgress = new TaskProgress(backpackTask);
            CalculationService.StartCalculation(backpackTask, taskProgress, token);

            if (token.IsCancellationRequested)
            {
                await unitOfWork.CompleteAsync();
                return;
            }
            backpackTask.EndTime = DateTime.Now;
            backpackTask.Complete = true;

            unitOfWork.CombinationSetRepository.RemoveRange(backpackTask.CombinationSets);
            await unitOfWork.CompleteAsync();
        }

        public async Task<List<BackpackTaskDto>> GetAllBackpackTaskWithRelatedObjectsAsync()
        {
            var unitOfWork = UnitOfWork.UnitOfWorkFactory();
            List<BackpackTask> tasks = await unitOfWork.BackpackTaskRepository.GetAllEagerLoadindAsync();
            List<BackpackTaskDto> list = Mapper.Map<List<BackpackTask>, List<BackpackTaskDto>>(tasks);
            return list;
        }

        public async Task<List<BackpackTaskDto>> GetAllBackpackTasksAsync()
        {
            var unitOfWork = UnitOfWork.UnitOfWorkFactory();
            List<BackpackTask> tasks = await unitOfWork.BackpackTaskRepository.GetAllAsync();
            List<BackpackTaskDto> list = Mapper.Map<List<BackpackTask>, List<BackpackTaskDto>>(tasks);
            return list;
        }

        public void ContinueInProgressBackpackTasks()
        {

            var unitOfWork = UnitOfWork.UnitOfWorkFactory();
            var listIds = unitOfWork.BackpackTaskRepository.GetInProgressTaskIds();
            foreach (var id in listIds)
            {
                HostingEnvironment.QueueBackgroundWorkItem(ct => StartBackpackTask(id, ct));
            }
        }

        public BackpackTaskDto GetBackpackTask(string id)
        {
            var unitOfWork = UnitOfWork.UnitOfWorkFactory();
            var backpackTask = unitOfWork.BackpackTaskRepository.Get(id);
            return Mapper.Map<BackpackTask, BackpackTaskDto>(backpackTask);
        }

        public async Task DelelteBackpackTask(string id)
        {
            var unitOfWork = UnitOfWork.UnitOfWorkFactory();
            var backpackTask = unitOfWork.BackpackTaskRepository.Get(id);
            unitOfWork.BackpackTaskRepository.Remove(backpackTask);
            await unitOfWork.CompleteAsync();
        }
    }
}