using System;
using System.Collections.Generic;
using EastBancTestAssignment.Core.Models;

namespace EastBancTestAssignment.BLL.DTOs
{
    public class BackpackTaskDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int WeightLimit { get; set; }
        public List<ItemDto> ItemDtos { get; set; }

        public int BestItemSetPrice { get; set; }
        public int BestItemSetWeight { get; set; }
        public List<ItemDto> BestItemDtosSet { get; set; }

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public double NumberOfUniqueItemCombination { get; set; }
        public double CombinationCalculated { get; set; }

        public List<ItemCombination> ItemCombinations { get; set; }

        public BackpackTaskDto()
        {
            BestItemDtosSet = new List<ItemDto>();
            ItemDtos = new List<ItemDto>();
        }
    }
}
