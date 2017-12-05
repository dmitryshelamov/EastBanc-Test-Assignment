using System;
using System.Collections.Generic;

namespace EastBancTestAssignment.Core.Models
{
    public class ItemCombinationSet
    {
        public string Id { get; set; }
        public bool IsCalculated { get; set; }

        public List<ItemCombination> ItemCombinations { get; set; }
        public BackpackTask BackpackTask { get; set; }
        public string BackpackTaskId { get; set; }

        public ItemCombinationSet()
        {
            Id = Guid.NewGuid().ToString();
            ItemCombinations = new List<ItemCombination>();
        }
    }
}
