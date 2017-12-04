using System;
using System.Collections.Generic;
using System.Linq;
using EastBancTestAssignment.BLL.DTOs;
using EastBancTestAssignment.BLL.Interfaces;
using EastBancTestAssignment.Core.Models;

namespace EastBancTestAssignment.BLL.Services
{
    public class BackpackTaskService : IBackpackTaskService
    {
        public BackpackTask CreateNewBackpackTask(List<ItemDto> itemDtos, string taskName, int backpackWeightLimit)
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
                Items = items,
                WeightLimit = backpackWeightLimit
            };

            //  and return it to client
            return backpackTask;
        }

        public void StartBackpackTask(BackpackTask backpackTask)
        {
            //  set start time
            backpackTask.StartTime = DateTime.Now;
            backpackTask.NumberOfUniqueItemCombination = Math.Pow(2, backpackTask.Items.Count) - 1;
            //  first we need to generate all possible unique sets of items
            if (backpackTask.ItemCombinations == null)
            {
                //  generate item combinations
                backpackTask.ItemCombinations = new List<ItemCombination>();
                GenerateCombination(backpackTask.Items, backpackTask.ItemCombinations);
            }

            CalculateBestItemSet(backpackTask);

            //  task done, update end time
            backpackTask.EndTime = DateTime.Now;
        }


        private void GenerateCombination(List<Item> set, List<ItemCombination> result)
        {
            result.Add(new ItemCombination { Items = set });
            GenerateCombinationRecursive(set, result);
        }

        private void GenerateCombinationRecursive(List<Item> set, List<ItemCombination> result)
        {
            for (int i = 0; i < set.Count; i++)
            {
                List<Item> temp = new List<Item>(set.Where((s, index) => index != i));

                if (temp.Count > 0 && !result.Where(l => l.Items.Count == temp.Count).Any(l => l.Items.SequenceEqual(temp)))
                {
                    result.Add(new ItemCombination { Items = temp });
                    GenerateCombinationRecursive(temp, result);
                }
            }
        }

        private void CalculateBestItemSet(BackpackTask backpackTask)
        {
            //  iterate over all item combinations
            foreach (var itemCombination in backpackTask.ItemCombinations)
            {
                //  iterate over all item int current item set
                //  calculate total weight and price of current item set
                var totalWeight = 0;
                var totalPrice = 0;
                foreach (var item in itemCombination.Items)
                {
                    totalWeight += item.Weight;
                    totalPrice += item.Price;
                }

                //  check if we ok with totalWeight and current price of item set is greater that current best item set price
                if (totalWeight <= backpackTask.WeightLimit &&
                    totalPrice > backpackTask.BestItemSetPrice)
                {
                    //  update current solution
                    backpackTask.BestItemSetPrice = totalPrice;
                    backpackTask.BestItemSetWeight = totalWeight;
                    backpackTask.BestItemsSet = itemCombination.Items;
                }

                //  mark current set as calucated
                itemCombination.IsCalculated = true;
                //  update calculation counter
                backpackTask.CombinationCalculated++;
            }
        }
    }
}
