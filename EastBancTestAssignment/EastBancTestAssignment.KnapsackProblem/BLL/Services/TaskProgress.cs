using System;
using EastBancTestAssignment.KnapsackProblem.Core.Models;

namespace EastBancTestAssignment.KnapsackProblem.BLL.Services
{
    public class TaskProgress
    {
        private readonly int _totalAmountOfWork;
        private int _currentProgress;

        public string BackpackTaskId { get; private set; }
        public int Progress { get; private set; }

        public TaskProgress(BackpackTask backpackTask)
        {
            BackpackTaskId = backpackTask.Id;
            _totalAmountOfWork = (int)Math.Round(Math.Pow(2, backpackTask.BackpackItems.Count) - 1);
            foreach (var combinationSet in backpackTask.CombinationSets)
            {
                if (combinationSet.IsCalculated)
                {
                    _currentProgress++;
                }
            }
        }

        public void UpdateProgress()
        {
            _currentProgress++;
            Progress = (int)Math.Round((double)(100 * _currentProgress) / _totalAmountOfWork);
        }
    }
}