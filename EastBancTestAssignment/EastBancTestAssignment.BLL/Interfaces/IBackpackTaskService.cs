using System.Collections.Generic;
using System.Threading.Tasks;
using EastBancTestAssignment.BLL.DTOs;
using EastBancTestAssignment.Core.Models;

namespace EastBancTestAssignment.BLL.Interfaces
{
    public interface IBackpackTaskService
    {
        Task<BackpackTaskDto> CreateNewBackpackTask(List<ItemDto> itemDtos, string taskName, int backpackWeightLimit);
        void StartBackpackTask(BackpackTaskDto backpackTask);
    }
}