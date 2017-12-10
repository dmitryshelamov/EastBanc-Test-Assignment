namespace EastBancTestAssignment.KnapsackProblem.UI.Hubs
{
    public class ProgressHubDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int? BestItemSetPrice { get; set; }
        public int Progress { get; set; }
        public string Status { get; set; }
    }
}