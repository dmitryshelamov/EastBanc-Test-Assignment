using System.Collections.Generic;
using EastBancTestAssignment.BLL.DTOs;
using EastBancTestAssignment.Core.Models;

namespace EastBancTestAssignment.BLL.Interfaces
{
    public interface IBackpackTaskService
    {
        BackpackTask CreateNewBackpackTask(List<ItemDto> itemDtos, string taskName, int backpackWeightLimit);
    }
}