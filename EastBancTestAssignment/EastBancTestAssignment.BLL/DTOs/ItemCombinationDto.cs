using System.Collections.Generic;
using EastBancTestAssignment.Core.Models;

namespace EastBancTestAssignment.BLL.DTOs
{
    public class ItemCombinationDto
    {
        public string Id { get; set; }
        public List<ItemDto> Items { get; set; }
        public bool IsCalculated { get; set; }

        public ItemCombinationDto()
        {
            Items = new List<ItemDto>();
        }
    }
}
