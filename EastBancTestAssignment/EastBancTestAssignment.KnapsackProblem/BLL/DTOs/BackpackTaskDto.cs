using System;
using System.Collections.Generic;

namespace EastBancTestAssignment.KnapsackProblem.BLL.DTOs
{
    public class BackpackTaskDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int WeightLimit { get; set; }
        public bool Complete { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public List<ItemDto> ItemDtos { get; set; }
        public List<ItemDto> BestItemSet { get; set; }
        public int PercentComplete { get; set; }
    }
}