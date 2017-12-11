using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EastBancTestAssignment.KnapsackProblem.UI.MVC.ViewModels
{
    public class BackpackTaskFormViewModel
    {
        [Required]
        [DisplayName("Task Name")]
        public string Name { get; set; }

        [Required, Range(1, int.MaxValue)]
        [DisplayName("Backpack Weight Limit")]
        public int? BackpackWeightLimit { get; set; }

        public List<ItemViewModel> Items { get; set; }

        public BackpackTaskFormViewModel()
        {
            Items = new List<ItemViewModel>();
        }
    }
}