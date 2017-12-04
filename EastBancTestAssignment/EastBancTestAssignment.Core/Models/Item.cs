using System;
using System.Collections.Generic;

namespace EastBancTestAssignment.Core.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public int Price { get; set; }

        public BackpackTask BackpackTask { get; set; }
        public List<BackpackTask> BestPrice { get; set; }
        public List<ItemCombination> ItemCombinations { get; set; }


        public Item()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
