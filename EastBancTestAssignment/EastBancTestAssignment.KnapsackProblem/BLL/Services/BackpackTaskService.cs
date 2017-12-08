using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EastBancTestAssignment.KnapsackProblem.BLL.DTOs;
using EastBancTestAssignment.KnapsackProblem.DAL.Interfaces;
using EastBancTestAssignment.KnapsackProblem.DAL.Models;

namespace EastBancTestAssignment.KnapsackProblem.BLL.Services
{
    public class BackpackTaskService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BackpackTaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

            _unitOfWork.BackpackTaskRepository.Add(backpackTask);
            await _unitOfWork.CompleteAsync();

            return backpackTask.Id;
        }

        public async Task StartBackpackTask(string id)
        {
            BackpackTask backpackTask = _unitOfWork.BackpackTaskRepository.Get(id);
            TaskProgress taskProgress = new TaskProgress(backpackTask);
            CalculationService.StartCalculation(backpackTask, taskProgress, _unitOfWork);

            backpackTask.Complete = true;
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<BackpackTaskDto>> GetAllBackpackTasksAsync()
        {
            List<BackpackTask> tasks = await _unitOfWork.BackpackTaskRepository.GetAllAsync();
            List<BackpackTaskDto> list = Mapper.Map<List<BackpackTask>, List<BackpackTaskDto>>(tasks);
            return list;
        }
    }
}