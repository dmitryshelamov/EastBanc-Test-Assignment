namespace EastBancTestAssignment.BLL.Services
{
    public class TaskCompleteEventArgs
    {
        public string Id { get; set; }
        public int WeightLimit { get; set; }
        public int BestItemPrice { get; set; }
        public int Percent { get; set; }
        public string Status { get; set; }
    }
}
