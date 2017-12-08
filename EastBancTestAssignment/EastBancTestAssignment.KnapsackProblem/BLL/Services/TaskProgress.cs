using System;
using System.Diagnostics;
using EastBancTestAssignment.KnapsackProblem.DAL.Models;
using EastBancTestAssignment.KnapsackProblem.UI.Hubs;
using Microsoft.AspNet.SignalR;

namespace EastBancTestAssignment.KnapsackProblem.BLL.Services
{
    public class TaskProgress
    {
        private readonly BackpackTask _backpackTask;
        private readonly int _totalAmountOfWork;
        private int _currentProgress;

        public string BackpackTaskId { get; private set; }
        public int Progress { get; private set; }

        public TaskProgress(BackpackTask backpackTask)
        {
            _backpackTask = backpackTask;
            BackpackTaskId = backpackTask.Id;
            _totalAmountOfWork = (int)Math.Round(Math.Pow(2, backpackTask.BackpackItems.Count) - 1);
            _currentProgress = backpackTask.CombinationSets.Count;
//            foreach (var combinationSet in backpackTask.CombinationSets)
//            {
//                if (combinationSet.IsCalculated)
//                {
//                    _currentProgress++;
//                }
//            }
        }

        public void UpdateProgress()
        {
            _currentProgress++;
            Progress = (int)Math.Round((double)(100 * _currentProgress) / _totalAmountOfWork);
            var hub = GlobalHost.ConnectionManager.GetHubContext<ProgressHub>();
            hub.Clients.All.ReportProgress(BackpackTaskId, Progress);
            Debug.WriteLine($"Id: {BackpackTaskId}, {Progress}");
            Debug.WriteLine($"Combination set done: {_backpackTask.CombinationSets.Count}, of {_totalAmountOfWork}");
        }
    }
}