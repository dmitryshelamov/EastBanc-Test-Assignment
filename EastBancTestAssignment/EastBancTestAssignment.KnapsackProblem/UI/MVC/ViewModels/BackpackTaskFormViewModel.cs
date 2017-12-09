using System.Collections.Generic;
using System.ComponentModel;

namespace EastBancTestAssignment.KnapsackProblem.UI.MVC.ViewModels
{
    public class BackpackTaskFormViewModel
    {
        [DisplayName("Task Name")]
        public string Name { get; set; }

        [DisplayName("Backpack Weight Limit")]
        public int BackpackWeightLimit { get; set; }

        public List<ItemViewModel> Items { get; set; }
    }
}