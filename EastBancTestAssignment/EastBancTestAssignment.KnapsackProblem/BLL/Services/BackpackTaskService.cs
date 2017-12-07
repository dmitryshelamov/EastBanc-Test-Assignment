using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EastBancTestAssignment.KnapsackProblem.BLL.Converters;
using EastBancTestAssignment.KnapsackProblem.BLL.DTOs;
using EastBancTestAssignment.KnapsackProblem.Core.Models;
using EastBancTestAssignment.KnapsackProblem.DAL.Interfaces;

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
                BackpackItems = CustomConverter.ItemDtosToItems(itemDtos),
                WeightLimit = backpackWeightLimit,
                StartTime = DateTime.Now
            };

            _unitOfWork.BackpackTaskRepository.Add(backpackTask);
            await _unitOfWork.CompleteAsync();

            return backpackTask.Id;
        }
    }
}