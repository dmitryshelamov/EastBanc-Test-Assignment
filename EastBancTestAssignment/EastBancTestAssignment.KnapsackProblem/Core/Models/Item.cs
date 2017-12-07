using System;

namespace EastBancTestAssignment.KnapsackProblem.Core.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public int Price { get; set; }

        public BackpackTask BackpackTask { get; set; }
        public string BackpackTaskId { get; set; }

        public Item()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}