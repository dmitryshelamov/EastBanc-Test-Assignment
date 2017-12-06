using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EastBancTestAssignment.BLL.Converters;
using EastBancTestAssignment.BLL.DTOs;
using EastBancTestAssignment.BLL.Interfaces;
using EastBancTestAssignment.Core.Models;
using EastBancTestAssignment.DAL;

namespace EastBancTestAssignment.BLL.Services
{
    public class BackpackTaskService : IBackpackTaskService
    {
        private static BackpackTaskService _instance;

        public EventHandler<TaskProgressEventArgs> OnUpdatProgressEventHandler;
        public EventHandler<TaskCompleteEventArgs> OnTaskCompleteEventHandler;

        private BackpackTaskService() { }

        public static BackpackTaskService GetInstance()
        {
            if (_instance == null)
                _instance = new BackpackTaskService();
            return _instance;
        }

        public async Task<BackpackTaskDto> NewBackpackTask(List<ItemDto> itemDtos, string taskName,
            int backpackWeightLimit)
        {
            //  create task
            BackpackTask backpackTask = new BackpackTask
            {
                Name = taskName,
                BackpackItems = Converter.ItemDtosToItems(itemDtos),
                WeightLimit = backpackWeightLimit,
                StartTime = DateTime.Now
        };

            //  save to database
            var unitOfWork = UnitOfWork.UnitOfWorkFactory();
            unitOfWork.BackpackTaskRepository.Add(backpackTask);
            await unitOfWork.CompleteAsync();

            //  and return it to client
            return Converter.ConvertToDto(backpackTask);
        }

        public async Task StartBackpackTask(BackpackTaskDto backpackTaskDto)
        {
            //  get backpack task from db
            var unitOfWork = UnitOfWork.UnitOfWorkFactory();
            BackpackTask backpackTask = await unitOfWork.BackpackTaskRepository.Get(backpackTaskDto.Id);

            ProgressService progressService = new ProgressService(backpackTask);

            int totalCombination = (int) Math.Round(Math.Pow(2, backpackTask.BackpackItems.Count) - 1);
            //  inspect backpackTask
            //  first we need to generate all possible unique sets of items
            if (backpackTask.ItemCombinationSets == null || backpackTask.ItemCombinationSets.Count != totalCombination)
            {
                //  generate item combinations
                backpackTask.ItemCombinationSets = new List<ItemCombinationSet>();
                GenerateCombination(backpackTask.BackpackItems, backpackTask.ItemCombinationSets, progressService);
            }
            //  get all calculated ItemCombinationSet
            else
            {
                foreach (var itemCombinationSet in backpackTask.ItemCombinationSets)
                {
                    if (itemCombinationSet.IsCalculated == false)
                    {
                        progressService.UpdateProgress();
                    }
                }
            }
            await unitOfWork.CompleteAsync();

            await CalculateBestItemSet(backpackTask, progressService);
            //  task done, update end time
            backpackTask.EndTime = DateTime.Now;
            backpackTask.Complete = true;
            await unitOfWork.CompleteAsync();


            if (OnTaskCompleteEventHandler != null)
            {
                OnTaskCompleteEventHandler(this, new TaskCompleteEventArgs()
                {
                    Id = backpackTask.Id,
                    WeightLimit = backpackTask.WeightLimit,
                    BestItemPrice = backpackTask.BestItemSetPrice,
                    Percent = 100,
                    Status = backpackTask.Complete.ToString()
                });
            }
        }

        private async Task CalculateBestItemSet(BackpackTask backpackTask, ProgressService service)
        {
            var unitOfWork = UnitOfWork.UnitOfWorkFactory();
            //  iterate over all item combinations
            foreach (var set in backpackTask.ItemCombinationSets)
            {
                if (set.IsCalculated == false)
                {
                    //  iterate over all item int current item set
                    //  calculate total weight and price of current item set
                    var totalWeight = 0;
                    var totalPrice = 0;
                    foreach (var itemCombination in set.ItemCombinations)
                    {
                        totalWeight += itemCombination.Item.Weight;
                        totalPrice += itemCombination.Item.Price;
                    }

                    //  check if we ok with totalWeight and current price of item set is greater that current best item set price
                    if (totalWeight <= backpackTask.WeightLimit &&
                        totalPrice > backpackTask.BestItemSetPrice)
                    {
                        //  update current solution
                        backpackTask.BestItemSetPrice = totalPrice;
                        backpackTask.BestItemSetWeight = totalWeight;
                        List<Item> items = new List<Item>();
                        foreach (var itemCombo in set.ItemCombinations)
                        {
                            items.Add(itemCombo.Item);
                        }
                        backpackTask.BestItemSet = new List<BackpackBestItemSet>();
                        foreach (var item in items)
                        {
                            backpackTask.BestItemSet.Add(new BackpackBestItemSet { Item = item });
                        }
                    }

                    //  mark current set as calucated
                    set.IsCalculated = true;

                    await unitOfWork.CompleteAsync();

                    service.UpdateProgress();
                    OnUpdatProgressEventHandler?.Invoke(this, new TaskProgressEventArgs()
                    {
                        Id = service.Id,
                        Percent = service.Percent
                    });

                }
            }
        }

        public async Task<List<BackpackTaskDto>> GetAllBackpackTasks()
        {
  
            List<BackpackTask> tasks = await UnitOfWork.UnitOfWorkFactory().BackpackTaskRepository.GetAll();
            List<BackpackTaskDto> backpackTaskDtos = new List<BackpackTaskDto>();
            foreach (var backpackTask in tasks)
            {
                backpackTaskDtos.Add(Converter.ConvertToDto(backpackTask));
            }

            return backpackTaskDtos;
        }

        public async Task<BackpackTaskDto> GetBackpackTask(string id)
        {
            BackpackTask backpack = await UnitOfWork.UnitOfWorkFactory().BackpackTaskRepository.Get(id);
            return Converter.ConvertToDto(backpack);
        }


        private void GenerateCombination(List<Item> set, List<ItemCombinationSet> result, ProgressService service)
        {
            result.Add(new ItemCombinationSet
            {
                ItemCombinations = set.Select(item => new ItemCombination { Item = item }).ToList()
            });
            service.UpdateProgress();
            OnUpdatProgressEventHandler?.Invoke(this, new TaskProgressEventArgs()
            {
                Id = service.Id,
                Percent = service.Percent
            });
            GenerateCombinationRecursive(set, result, service);
        }

        private void GenerateCombinationRecursive(List<Item> set, List<ItemCombinationSet> result, ProgressService service)
        {
            for (int i = 0; i < set.Count; i++)
            {
                List<Item> temp = new List<Item>(set.Where((s, index) => index != i));

                if (temp.Count > 0 && !result.Where(l => l.ItemCombinations.Count == temp.Count).Any(l =>
                {
                    List<Item> items = l.ItemCombinations.Select(argItemCombination => argItemCombination.Item).ToList();
                    return items.SequenceEqual(temp);
                }))
                {
                    result.Add(new ItemCombinationSet { ItemCombinations = temp.Select(item => new ItemCombination { Item = item }).ToList() });
                    GenerateCombinationRecursive(temp, result, service);
                    service.UpdateProgress();
                    OnUpdatProgressEventHandler?.Invoke(this, new TaskProgressEventArgs()
                    {
                        Id = service.Id,
                        Percent = service.Percent
                    });
                }
            }
        }



        public async Task Delete(string id)
        {
            var unitOfWork = UnitOfWork.UnitOfWorkFactory();
            await unitOfWork.BackpackTaskRepository.Remove(id);
            await unitOfWork.CompleteAsync();
        }
    }
}
