using System.Collections.Generic;

namespace EastBancTestAssignment.Core.Models
{
    public class BackpackTask
    {
        public string Id { get; set; }
        public Backpack Backpack { get; set; }
        public List<Item> Items { get; set; }   
    }
}
