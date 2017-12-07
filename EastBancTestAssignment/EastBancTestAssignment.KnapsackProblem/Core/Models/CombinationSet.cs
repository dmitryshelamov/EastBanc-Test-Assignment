using System;
using System.Collections.Generic;

namespace EastBancTestAssignment.KnapsackProblem.Core.Models
{
    public class CombinationSet
    {
        public string Id { get; set; }
        public bool IsCalculated { get; set; }

        public List<ItemCombination> ItemCombinations { get; set; }
        public BackpackTask BackpackTask { get; set; }
        public string BackpackTaskId { get; set; }

        public CombinationSet()
        {
            Id = Guid.NewGuid().ToString();
            ItemCombinations = new List<ItemCombination>();
        }
    }
}