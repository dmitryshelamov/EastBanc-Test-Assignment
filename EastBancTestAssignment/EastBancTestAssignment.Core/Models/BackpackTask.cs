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
        public BackpackTaskSolution BackpackTaskSolution { get; set; }

        public BackpackTask()
        {
            BackpackTaskSolution = new BackpackTaskSolution();
            Id = Guid.NewGuid().ToString();
            Items = new List<Item>();
        }
    }
}
