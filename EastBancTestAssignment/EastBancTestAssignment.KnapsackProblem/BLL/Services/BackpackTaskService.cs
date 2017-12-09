using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EastBancTestAssignment.KnapsackProblem.BLL.DTOs;
using EastBancTestAssignment.KnapsackProblem.DAL;
using EastBancTestAssignment.KnapsackProblem.DAL.Models;

namespace EastBancTestAssignment.KnapsackProblem.BLL.Services
{
    public class BackpackTaskService
    {
        public static Dictionary<string, CancellationTokenSource> CancellationTokens;

        static BackpackTaskService()
        {
            CancellationTokens = new Dictionary<string, CancellationTokenSource>();
        }

        public BackpackTaskService()
        {
            
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
            await CalculationService.StartCalculation(backpackTask, taskProgress, unitOfWork, token);

            if (token.IsCancellationRequested)
            {
                return;
            }
            backpackTask.EndTime = DateTime.Now;
            backpackTask.Complete = true;
            CancellationTokens.Remove(id);
            await unitOfWork.CompleteAsync();
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
                CancellationTokenSource cancellation = new CancellationTokenSource();
                CancellationToken token = cancellation.Token;
                CancellationTokens.Add(id, cancellation);
                Task.Run(() => StartBackpackTask(id, token));
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
            //  get cancellation token
            if (backpackTask.Complete == false)
            {
                CancellationTokenSource cancellationToken = CancellationTokens[backpackTask.Id];
                cancellationToken.Cancel();
            }
            unitOfWork.BackpackTaskRepository.Remove(backpackTask);
            await unitOfWork.CompleteAsync();
        }
    }
}