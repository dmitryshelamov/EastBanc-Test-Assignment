﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EastBancTestAssignment.KnapsackProblem.UI.MVC.ViewModels
{
    public class BackpackTaskDetailViewModel
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