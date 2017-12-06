using System;
using System.Collections.Generic;
using System.Linq;
using EastBancTestAssignment.BLL.DTOs;
using EastBancTestAssignment.Core.Models;

namespace EastBancTestAssignment.BLL.Converters
{
    public static class Converter
    {
        public static List<Item> ItemDtosToItems(List<ItemDto> listDtos)
        {
            return listDtos.Select(itemDto => new Item
            {
                Name = itemDto.Name,
                Price = itemDto.Price,
                Weight = itemDto.Weight
            }).ToList();
        }

        public static BackpackTaskDto ConvertToDto(BackpackTask backpackTask)
        {
            BackpackTaskDto backpackTaskDto = new BackpackTaskDto();
            backpackTaskDto.Id = backpackTask.Id;
            backpackTaskDto.Name = backpackTask.Name;
            backpackTaskDto.WeightLimit = backpackTask.WeightLimit;
            backpackTaskDto.ItemDtos = backpackTask.BackpackItems.Select(bi => new ItemDto
            {
                Id = bi.Id,
                Name = bi.Name,
                Price = bi.Price,
                Weight = bi.Weight
            }).ToList();
            backpackTaskDto.BestItemSetPrice = backpackTask.BestItemSetPrice;
            backpackTaskDto.BestItemSetWeight = backpackTask.BestItemSetWeight;
            backpackTaskDto.BestItemDtosSet = backpackTask.BestItemSet.Select(bis => new ItemDto
            {
                Id = bis.Item.Id,
                Name = bis.Item.Name,
                Price = bis.Item.Price,
                Weight = bis.Item.Weight
            }).ToList();
            backpackTaskDto.StartTime = backpackTask.StartTime;
            backpackTaskDto.EndTime = backpackTask.EndTime;
            var totalCombination = (int) Math.Round(Math.Pow(2, backpackTask.BackpackItems.Count) - 1);
            backpackTaskDto.TotalAmoutOfWork =
                totalCombination * 2;

            var currentProgress = 0;

            if (backpackTask.Complete)
            {
                currentProgress = backpackTaskDto.TotalAmoutOfWork;
            }
            else
            {
                currentProgress = backpackTask.ItemCombinationSets.Count;
                foreach (var itemCombinationSet in backpackTask.ItemCombinationSets)
                {
                    if (itemCombinationSet.IsCalculated)
                    {
                        currentProgress++;
                    }
                }
            }

            backpackTaskDto.CurrentProgress = currentProgress;
            backpackTaskDto.ItemCombinationDtos = backpackTask.ItemCombinationSets.Select(ic => new ItemCombinationDto
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
            }).ToList();
            backpackTaskDto.Complete = backpackTask.Complete;

            return backpackTaskDto;
        }
    }
}
