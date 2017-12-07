using System.Collections.Generic;

namespace EastBancTestAssignment.KnapsackProblem.UI.MVC.ViewModels
{
    public class BackpackTaskFormViewModel
    {
        public string Name { get; set; }
        public int BackpackWeightLimit { get; set; }
        public List<ItemViewModel> Items { get; set; }
    }
}