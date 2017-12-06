using System;
using EastBancTestAssignment.Core.Models;

namespace EastBancTestAssignment.BLL.Services
{
    public class ProgressService
    {
        public EventHandler<TaskProgressEventArgs> OnUpdatProgressEventHandler;

        private readonly BackpackTask _backpackTask;

        private readonly int _totalAmountOfWork;
        private int _currentProgress;



        public int Percent { get; private set; }
        public string Id { get; }


        public ProgressService(BackpackTask backpackTask)
        {
            _currentProgress = 0;
            _backpackTask = backpackTask;
            Id = backpackTask.Id;
            int totalCombinations = (int) Math.Round(Math.Pow(2, backpackTask.BackpackItems.Count) - 1);
            _totalAmountOfWork = totalCombinations * 2;
            if (backpackTask.ItemCombinationSets.Count == totalCombinations)
            {
                _currentProgress = totalCombinations;
            }
        }

        public void UpdateProgress()
        {
            _currentProgress++;
            Percent = (int) Math.Round((double) (100 * _currentProgress) / _totalAmountOfWork);
            if (OnUpdatProgressEventHandler != null)
            {
                OnUpdatProgressEventHandler(this, new TaskProgressEventArgs()
                {
                    Id = _backpackTask.Id,
                    Percent = Percent
                });
            }
        }
    }
}
