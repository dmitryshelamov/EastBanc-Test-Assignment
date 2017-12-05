namespace EastBancTestAssignment.Core.Models
{
    public class ItemCombination
    {
        public Item Item { get; set; }
        public string ItemId { get; set; }

        public ItemCombinationSet ItemCombinationSet { get; set; }
        public string ItemCombinationSetId { get; set; }
    }
}
