using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EastBancTestAssignment.KnapsackProblem.BLL.DTOs;
using EastBancTestAssignment.KnapsackProblem.BLL.Services;
using Microsoft.AspNet.SignalR;

namespace EastBancTestAssignment.KnapsackProblem.UI.Hubs
{
    public class ProgressHub : Hub
    {
        public async Task RequestUpdate()
        {
            var service = new BackpackTaskService();
            List<BackpackTaskDto> tasks = await service.GetAllBackpackTasksAsync();
            List<ProgressHubDto> progressList = new List<ProgressHubDto>();
            foreach (var backpackTaskDto in tasks)
            {
                string status = backpackTaskDto.Complete ? "Complete" : "In Progress";
                int? bestItemPrice = backpackTaskDto.Complete ? backpackTaskDto.BestItemSetPrice : default(int?);
                progressList.Add(new ProgressHubDto
                {
                    Id = backpackTaskDto.Id,
                    Name = backpackTaskDto.Name,
                    BestItemSetPrice = bestItemPrice,
                    Progress = backpackTaskDto.PercentComplete,
                    Status = status
                });
            }
            Clients.All.ReportProgressExtended(progressList);
        }
    }
}