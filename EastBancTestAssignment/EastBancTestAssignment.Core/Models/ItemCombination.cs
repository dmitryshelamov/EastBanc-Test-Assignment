using System.Collections.Generic;

namespace EastBancTestAssignment.Core.Models
{
    public class ItemCombination
    {
        public string Id { get; set; }
        public bool IsCalculated { get; set; }

        public List<Item> Items { get; set; }

        public ItemCombination()
        {
            Items = new List<Item>();
        }
    }
}
