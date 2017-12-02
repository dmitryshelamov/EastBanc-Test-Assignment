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

            BackpackTask backpackTask = new BackpackTask
            {
                Name = taskName,
                Items = items,
                WeightLimit = backpackWeightLimit
            };

            return backpackTask;
        }
    }
}
