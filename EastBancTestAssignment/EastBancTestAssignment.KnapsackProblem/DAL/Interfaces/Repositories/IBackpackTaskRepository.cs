using EastBancTestAssignment.KnapsackProblem.DAL.Models;

namespace EastBancTestAssignment.KnapsackProblem.DAL.Interfaces.Repositories
{
    public interface IBackpackTaskRepository
    {
        void Add(BackpackTask backpackTask);
        BackpackTask Get(string id);
    }
}