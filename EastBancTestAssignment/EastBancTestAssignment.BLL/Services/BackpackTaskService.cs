﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EastBancTestAssignment.BLL.DTOs;
using EastBancTestAssignment.BLL.Interfaces;
using EastBancTestAssignment.Core.Models;
using EastBancTestAssignment.DAL;

namespace EastBancTestAssignment.BLL.Services
{
    public class BackpackTaskService : IBackpackTaskService
    {
        public EventHandler<TaskProgressEventArgs> OnUpdatProgressEventHandler;
        public EventHandler<TaskCompleteEventArgs> OnTaskCompleteEventHandler;
        //        private readonly IUnitOfWork _unitOfWork;

        public List<string> InPtogressBackpackTaskIds { get; set; }

        private BackpackTaskService()
        {
            //            _unitOfWork = new UnitOfWork(new AppDbContext());
            InPtogressBackpackTaskIds = new List<string>();
        }

        private static BackpackTaskService _instance;

        public static BackpackTaskService GetInstance()
        {
            if (_instance == null)
                _instance = new BackpackTaskService();
            return _instance;
        }

        public async Task<BackpackTaskDto> CreateNewBackpackTask(List<ItemDto> itemDtos, string taskName,
            int backpackWeightLimit)
        {
            //  convert itemDtos to items
            List<Item> items = itemDtos.Select(itemDto => new Item
            {
                Name = itemDto.Name,
                Price = itemDto.Price,
                Weight = itemDto.Weight
            }).ToList();
            //  create task
            BackpackTask backpackTask = new BackpackTask
            {
                Name = taskName,
                BackpackItems = items,
                WeightLimit = backpackWeightLimit
            };

            //  save to databaser
            var unitOfWork = UnitOfWork.UnitOfWorkFactory();
            unitOfWork.BackpackTaskRepository.Add(backpackTask);
            await unitOfWork.CompleteAsync();

            //  and return it to client
            return new BackpackTaskDto
            {
                Id = backpackTask.Id,
                Name = backpackTask.Name,
                ItemDtos = itemDtos,
                WeightLimit = backpackTask.WeightLimit
            };
        }

        public async Task StartBackpackTask(BackpackTaskDto backpackTaskDto)
        {
            var unitOfWork = UnitOfWork.UnitOfWorkFactory();
            BackpackTask backpackTask = await unitOfWork.BackpackTaskRepository.Get(backpackTaskDto.Id);
            //  set start time
            backpackTask.StartTime = DateTime.Now;
            //  first we need to generate all possible unique sets of items
            if (backpackTask.ItemCombinationSets == null || backpackTask.ItemCombinationSets.Count == 0)
            {
                //  generate item combinations
                backpackTask.ItemCombinationSets = new List<ItemCombinationSet>();
                List<Item> items = backpackTask.BackpackItems;
                List<List<Item>> result = new List<List<Item>>();
                GenerateCombination(items, result);
                foreach (var combination in result)
                {
                    List<ItemCombination> itemCombinations = new List<ItemCombination>();
                    foreach (var item in combination)
                    {
                        itemCombinations.Add(new ItemCombination { Item = item });
                    }

                    backpackTask.ItemCombinationSets.Add(new ItemCombinationSet() { ItemCombinations = itemCombinations });
                }
            }


            CalculateBestItemSet(backpackTask, unitOfWork);
            //  task done, update end time
            backpackTask.EndTime = DateTime.Now;
            backpackTask.Complete = true;

            await unitOfWork.CompleteAsync();
            Debug.WriteLine($"Remove task from List {backpackTaskDto.Id}");
            InPtogressBackpackTaskIds.Remove(backpackTaskDto.Id);

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

        public async Task<List<BackpackTaskDto>> GetAllBackpackTasks()
        {
  
            List<BackpackTask> tasks = await UnitOfWork.UnitOfWorkFactory().BackpackTaskRepository.GetAll();
            List<BackpackTaskDto> backpackTaskDtos = new List<BackpackTaskDto>();
            foreach (var backpackTask in tasks)
            {
                backpackTaskDtos.Add(ConvertToDto(backpackTask));
            }

            return backpackTaskDtos;
        }

        public async Task<BackpackTaskDto> GetBackpackTask(string id)
        {
            BackpackTask backpack = await UnitOfWork.UnitOfWorkFactory().BackpackTaskRepository.Get(id);
            return ConvertToDto(backpack);
        }

        private static BackpackTaskDto ConvertToDto(BackpackTask backpackTask)
        {
           return new BackpackTaskDto
            {
                Id = backpackTask.Id,
                Name = backpackTask.Name,
                WeightLimit = backpackTask.WeightLimit,
                ItemDtos = backpackTask.BackpackItems.Select(bi => new ItemDto
                {
                    Id = bi.Id,
                    Name = bi.Name,
                    Price = bi.Price,
                    Weight = bi.Weight
                }).ToList(),
                BestItemSetPrice = backpackTask.BestItemSetPrice,
                BestItemSetWeight = backpackTask.BestItemSetWeight,
                BestItemDtosSet = backpackTask.BestItemSet.Select(bis => new ItemDto
                {
                    Id = bis.Item.Id,
                    Name = bis.Item.Name,
                    Price = bis.Item.Price,
                    Weight = bis.Item.Weight
                }).ToList(),
                StartTime = backpackTask.StartTime,
                EndTime = backpackTask.EndTime,
                NumberOfUniqueItemCombination = (int)Math.Round(Math.Pow(2, backpackTask.ItemCombinationSets.Count) - 1),
                CombinationCalculated = backpackTask.CombinationCalculated,
                ItemCombinationDtos = backpackTask.ItemCombinationSets.Select(ic => new ItemCombinationDto
                {
                    Id = ic.Id,
                    IsCalculated = ic.IsCalculated,
                    ItemDtos = ic.ItemCombinations.Select(c => new ItemDto
                    {
                        Id = c.Item.Id,
                        Name = c.Item.Name,
                        Price = c.Item.Price,
                        Weight = c.Item.Weight
                    }).ToList()
                }).ToList(),
                Complete = backpackTask.Complete
            };
        }



        private void GenerateCombination(List<Item> set, List<List<Item>> result)
        {
            result.Add(set);
            GenerateCombinationRecursive(set, result);
        }

        private void GenerateCombinationRecursive(List<Item> set, List<List<Item>> result)
        {
            for (int i = 0; i < set.Count; i++)
            {
                List<Item> temp = new List<Item>(set.Where((s, index) => index != i));

                if (temp.Count > 0 && !result.Where(l => l.Count == temp.Count).Any(l => l.SequenceEqual(temp)))
                {
                    result.Add(temp);
                    GenerateCombinationRecursive(temp, result);
                }
            }
        }

        private void CalculateBestItemSet(BackpackTask backpackTask, UnitOfWork unitOfWork)
        {

            //  iterate over all item combinations
            foreach (var set in backpackTask.ItemCombinationSets)
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
                //  update calculation counter
                backpackTask.CombinationCalculated++;

                int percentage = (int)Math.Round((double)(100 * backpackTask.CombinationCalculated) /
                                                 backpackTask.ItemCombinationSets.Count);
                if (percentage < 0)
                    percentage = 0;
                if (percentage > 100)
                    percentage = 100;

                Debug.WriteLine($"Percant: {(int)percentage}%");
                if (OnUpdatProgressEventHandler != null)
                {
                    OnUpdatProgressEventHandler(this, new TaskProgressEventArgs()
                    {
                        Id = backpackTask.Id,
                        Percent = percentage
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
