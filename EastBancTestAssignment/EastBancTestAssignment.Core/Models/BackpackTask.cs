using System;
using System.Collections.Generic;

namespace EastBancTestAssignment.Core.Models
{
    public class BackpackTask
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int WeightLimit { get; set; }
        public List<Item> Items { get; set; }

        public int BestItemSetPrice { get; set; }
        public int BestItemSetWeight { get; set; }
        public List<Item> BestItemsSet { get; set; }

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public double NumberOfUniqueItemCombination { get; set; }
        public double CombinationCalculated { get; set; }

        public List<ItemCombination> ItemCombinations { get; set; }

        public BackpackTask()
        {
            BestItemsSet = new List<Item>();
            Id = Guid.NewGuid().ToString();
            Items = new List<Item>();
        }
    }
}
