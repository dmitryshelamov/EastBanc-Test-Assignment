using System;
using System.Collections.Generic;

namespace EastBancTestAssignment.Web.UI.MVC.ViewModels
{
    public class BackpackTaskSolutionViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int WeightLimit { get; set; }
        public string Status { get; set; }
        public List<ItemViewModel> Items { get; set; }

        public int BestPrice { get; set; }
        public int BestItemSetWeight { get; set; }
        public List<ItemViewModel> BestItemSet { get; set; }
        public TimeSpan? CalculationTime { get; set; }
    }
}