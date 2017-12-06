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

        public int TotalAmoutOfWork { get; set; }
        public int CurrentProgress { get; set; }
        public bool Complete { get; set; }

        public List<ItemCombinationDto> ItemCombinationDtos { get; set; }

        public BackpackTaskDto()
        {
            BestItemDtosSet = new List<ItemDto>();
            ItemDtos = new List<ItemDto>();
        }
    }
}
