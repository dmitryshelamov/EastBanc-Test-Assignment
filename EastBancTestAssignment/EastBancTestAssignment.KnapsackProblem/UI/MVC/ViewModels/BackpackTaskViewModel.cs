﻿namespace EastBancTestAssignment.KnapsackProblem.UI.MVC.ViewModels
{
    public class BackpackTaskViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int WeightLimit { get; set; }
        public int? BestPrice { get; set; }
        public int? PercentComplete { get; set; }
        public bool Complete { get; set; }
        public string StatusMessage { get; set; }
    }
}