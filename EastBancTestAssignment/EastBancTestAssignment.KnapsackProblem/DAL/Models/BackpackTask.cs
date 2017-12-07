using System;
using System.Collections.Generic;

namespace EastBancTestAssignment.KnapsackProblem.DAL.Models
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
        public bool Complete { get; set; }

        public List<Item> BackpackItems { get; set; }
        public List<BestItemSet> BestItemSet { get; set; }
        public List<CombinationSet> CombinationSets { get; set; }

        public BackpackTask()
        {
            Id = Guid.NewGuid().ToString();
            BackpackItems = new List<Item>();
            BestItemSet = new List<BestItemSet>();
            CombinationSets = new List<CombinationSet>();
        }
    }
}