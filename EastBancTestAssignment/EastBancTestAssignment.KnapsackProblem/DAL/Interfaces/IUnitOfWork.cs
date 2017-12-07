using System.Threading.Tasks;
using EastBancTestAssignment.KnapsackProblem.DAL.Interfaces.Repositories;

namespace EastBancTestAssignment.KnapsackProblem.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IBackpackTaskRepository BackpackTaskRepository { get; }
        Task CompleteAsync();
    }
}