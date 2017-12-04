using System;
using System.Collections.Generic;

namespace EastBancTestAssignment.Core.Models
{
    public class ItemCombination
    {
        public string Id { get; set; }
        public bool IsCalculated { get; set; }

        public List<Item> Items { get; set; }
        public BackpackTask BackpackTask { get; set; }


        public ItemCombination()
        {
            Id = Guid.NewGuid().ToString();
            Items = new List<Item>();
        }
    }
}
