using System.Collections.Generic;
using System.Linq;
using EastBancTestAssignment.KnapsackProblem.BLL.DTOs;
using EastBancTestAssignment.KnapsackProblem.DAL.Models;

namespace EastBancTestAssignment.KnapsackProblem.BLL.Converters
{
    public static class CustomConverter
    {
        public static List<Item> ItemDtosToItems(List<ItemDto> listDtos)
        {
            return listDtos.Select(itemDto => new Item
            {
                Name = itemDto.Name,
                Price = itemDto.Price,
                Weight = itemDto.Weight
            }).ToList();
        }
    }
}