using System;
using System.Collections.Generic;

namespace EastBancTestAssignment.Core.Models
{
    public class BackpackTask
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int WeightLimit { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int BestItemSetPrice { get; set; }
        public int BestItemSetWeight { get; set; }
        public int CombinationCalculated { get; set; }
        public bool Complete { get; set; }

        public List<Item> BackpackItems { get; set; }
        public List<BackpackBestItemSet> BestItemSet { get; set; }
        public List<ItemCombinationSet> ItemCombinationSets { get; set; }

        public BackpackTask()
        {
            Id = Guid.NewGuid().ToString();
            BackpackItems = new List<Item>();
            BestItemSet = new List<BackpackBestItemSet>();
            ItemCombinationSets = new List<ItemCombinationSet>();
        }
    }
}
