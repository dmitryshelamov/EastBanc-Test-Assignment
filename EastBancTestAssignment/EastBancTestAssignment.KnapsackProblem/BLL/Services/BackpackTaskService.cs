using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EastBancTestAssignment.KnapsackProblem.BLL.DTOs;
using EastBancTestAssignment.KnapsackProblem.DAL;
using EastBancTestAssignment.KnapsackProblem.DAL.Interfaces;
using EastBancTestAssignment.KnapsackProblem.DAL.Models;
using EastBancTestAssignment.KnapsackProblem.DAL.Repositories;

namespace EastBancTestAssignment.KnapsackProblem.BLL.Services
{
    public class BackpackTaskService
    {
//        private readonly IUnitOfWork _unitOfWork;

        public BackpackTaskService(IUnitOfWork unitOfWork)
        {
//            _unitOfWork = unitOfWork;
//            ContinueInProgressBackpackTasks();
        }

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

            var unitOfWork = new UnitOfWork(new AppDbContext());
            unitOfWork.BackpackTaskRepository.Add(backpackTask);
            await unitOfWork.CompleteAsync();

            return backpackTask.Id;
        }

        public async Task StartBackpackTask(string id)
        {
            var unitOfWork = new UnitOfWork(new AppDbContext());
            BackpackTask backpackTask = unitOfWork.BackpackTaskRepository.Get(id);
            TaskProgress taskProgress = new TaskProgress(backpackTask);
            await CalculationService.StartCalculation(backpackTask, taskProgress, unitOfWork);

            backpackTask.EndTime = DateTime.Now;
            backpackTask.Complete = true;
            await unitOfWork.CompleteAsync();
        }

        public async Task<List<BackpackTaskDto>> GetAllBackpackTasksAsync()
        {
            var unitOfWork = new UnitOfWork(new AppDbContext());
            List<BackpackTask> tasks = await unitOfWork.BackpackTaskRepository.GetAllAsync();
            List<BackpackTaskDto> list = Mapper.Map<List<BackpackTask>, List<BackpackTaskDto>>(tasks);
            return list;
        }

        public void ContinueInProgressBackpackTasks()
        {
            var unitOfWork = new UnitOfWork(new AppDbContext());
            var listIds = unitOfWork.BackpackTaskRepository.GetInProgressTaskIds();
            foreach (var id in listIds)
            {
                Task.Run(() => StartBackpackTask(id));
            }
        }

        public BackpackTaskDto GetBackpackTask(string id)
        {
            var unitOfWork = new UnitOfWork(new AppDbContext());
            var backpackTask = unitOfWork.BackpackTaskRepository.Get(id);
            return Mapper.Map<BackpackTask, BackpackTaskDto>(backpackTask);
        }

        public async Task DelelteBackpackTask(string id)
        {
            var unitOfWork = new UnitOfWork(new AppDbContext());
            var backpackTask = unitOfWork.BackpackTaskRepository.Get(id);
            unitOfWork.BackpackTaskRepository.Remove(backpackTask);
            await unitOfWork.CompleteAsync();
        }
    }
}