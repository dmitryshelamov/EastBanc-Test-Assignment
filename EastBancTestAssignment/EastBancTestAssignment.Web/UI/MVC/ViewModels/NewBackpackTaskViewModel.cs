using System.Collections.Generic;
using System.ComponentModel;

namespace EastBancTestAssignment.Web.UI.MVC.ViewModels
{
    public class NewBackpackTaskViewModel
    {
        public string Name { get; set; }
        [DisplayName("Backpack Weight Limit")]
        public int BackpackWeightLimit { get; set; }
        public List<ItemViewModel> Items { get; set; }
    }
}