using System.Collections.Generic;
using System.Threading.Tasks;
using EastBancTestAssignment.KnapsackProblem.DAL.Models;

namespace EastBancTestAssignment.KnapsackProblem.DAL.Interfaces.Repositories
{
    public interface IBackpackTaskRepository
    {
        void Add(BackpackTask backpackTask);
        BackpackTask Get(string id);
        Task<List<BackpackTask>> GetAllAsync();
    }
}