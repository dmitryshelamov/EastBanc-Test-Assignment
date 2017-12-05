namespace EastBancTestAssignment.Core.Models
{
    public class BackpackBestItemSet
    {
        public Item Item { get; set; }
        public string ItemId { get; set; }

        public BackpackTask BackpackTask { get; set; }
        public string BackpackTaskId { get; set; }
    }
}
