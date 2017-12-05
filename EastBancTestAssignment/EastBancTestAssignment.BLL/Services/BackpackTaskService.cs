using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EastBancTestAssignment.BLL.DTOs;
using EastBancTestAssignment.BLL.Interfaces;
using EastBancTestAssignment.Core.Models;
using EastBancTestAssignment.DAL;
using EastBancTestAssignment.DAL.Interfaces;

namespace EastBancTestAssignment.BLL.Services
{
    public class BackpackTaskService : IBackpackTaskService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BackpackTaskService()
        {
            _unitOfWork = new UnitOfWork(new AppDbContext());
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
            _unitOfWork.BackpackTaskRepository.Add(backpackTask);
            await _unitOfWork.CompleteAsync();

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
            BackpackTask backpackTask = await _unitOfWork.BackpackTaskRepository.Get(backpackTaskDto.Id);
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

            await CalculateBestItemSet(backpackTask);

            //  task done, update end time
            backpackTask.EndTime = DateTime.Now;

            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<BackpackTaskDto>> GetAllBackpackTasks()
        {
            List<BackpackTask> tasks = await _unitOfWork.BackpackTaskRepository.GetAll();
            List<BackpackTaskDto> backpackTaskDtos = new List<BackpackTaskDto>();
            foreach (var backpackTask in tasks)
            {
                backpackTaskDtos.Add(ConvertToDto(backpackTask));
            }

            return backpackTaskDtos;
        }

        public async Task<BackpackTaskDto> GetBackpackTask(string id)
        {
            BackpackTask backpack = await _unitOfWork.BackpackTaskRepository.Get(id);
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
                BestItemDtosSet = backpackTask.BackpackItems.Select(bis => new ItemDto
                {
                    Id = bis.Id,
                    Name = bis.Name,
                    Price = bis.Price,
                    Weight = bis.Weight
                }).ToList(),
                StartTime = backpackTask.StartTime,
                EndTime = backpackTask.EndTime,
                NumberOfUniqueItemCombination = Math.Pow(2, backpackTask.ItemCombinationSets.Count) - 1,
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
                }).ToList()
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

        private async Task CalculateBestItemSet(BackpackTask backpackTask)
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

                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task Delete(string id)
        {
            await _unitOfWork.BackpackTaskRepository.Remove(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}
