namespace EastBancTestAssignment.KnapsackProblem.Core.Models
{
    public class ItemCombination
    {
        public Item Item { get; set; }
        public string ItemId { get; set; }

        public CombinationSet CombinationSet { get; set; }
        public string ItemCombinationSetId { get; set; }
    }
}