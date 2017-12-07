using EastBancTestAssignment.KnapsackProblem.Core.Models;

namespace EastBancTestAssignment.KnapsackProblem.DAL.Interfaces.Repositories
{
    public interface IBackpackTaskRepository
    {
        void Add(BackpackTask backpackTask);
    }
}