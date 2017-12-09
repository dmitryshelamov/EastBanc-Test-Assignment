using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace EastBancTestAssignment.KnapsackProblem.UI.MVC.ViewModels
{
    public class BackpackTaskDetailViewModel
    {
        public string Id { get; set; }

        [DisplayName("Task Name")]
        public string Name { get; set; }

        [DisplayName("Backpack Weight Limit")]
        public int WeightLimit { get; set; }

        [DisplayName("Task Status")]
        public string Status { get; set; }

        [DisplayName("Backpack Items")]
        public List<ItemViewModel> Items { get; set; }

        [DisplayName("Best Items Price")]
        public int BestPrice { get; set; }

        public List<ItemViewModel> BestItemSet { get; set; }

        [DisplayName("Task Calculation Time")]
        public TimeSpan? CalculationTime { get; set; }
    }
}