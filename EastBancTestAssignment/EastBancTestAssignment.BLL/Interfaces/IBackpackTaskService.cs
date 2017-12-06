using System.Collections.Generic;
using System.Threading.Tasks;
using EastBancTestAssignment.BLL.DTOs;
using EastBancTestAssignment.Core.Models;

namespace EastBancTestAssignment.BLL.Interfaces
{
    public interface IBackpackTaskService
    {
        Task<BackpackTaskDto> NewBackpackTask(List<ItemDto> itemDtos, string taskName, int backpackWeightLimit);
        Task StartBackpackTask(BackpackTaskDto backpackTaskDto);
    }
}