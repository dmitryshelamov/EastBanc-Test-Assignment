namespace EastBancTestAssignment.KnapsackProblem.UI.MVC.ViewModels
{
    public class BackpackTaskViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int BackpackWeightLimit { get; set; }
        public int BestPrice { get; set; }
        public string PercentComplete { get; set; }
        public string Status { get; set; }
    }
}