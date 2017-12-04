using System.Collections.Generic;
using EastBancTestAssignment.Core.Models;

namespace EastBancTestAssignment.BLL.DTOs
{
    class ItemCombinationDto
    {
        public string Id { get; set; }
        public bool IsCalculated { get; set; }

        public List<ItemDto> ItemDtos { get; set; }

        public ItemCombinationDto()
        {
            ItemDtos = new List<ItemDto>();
        }
    }
}
