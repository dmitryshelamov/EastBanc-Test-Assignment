using System;
using System.Collections.Generic;
using EastBancTestAssignment.Core.Models;

namespace EastBancTestAssignment.BLL.DTOs
{
    public class BackpackTaskSolutionDto
    {
        public int BestItemSetPrice { get; set; }
        public int BestItemSetWeight { get; set; }
        public List<ItemDto> BestItemDtosSet { get; set; }

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public double NumberOfUniqueItemCombination { get; set; }
        public double CombinationCalculated { get; set; }

        public List<ItemCombinationDto> ItemCombinationDtos { get; set; }

        public BackpackTaskSolutionDto()
        {
            BestItemDtosSet = new List<ItemDto>();
        }
    }
}
