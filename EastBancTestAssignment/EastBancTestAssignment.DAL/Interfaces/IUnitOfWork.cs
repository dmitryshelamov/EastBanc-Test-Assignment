using System.Threading.Tasks;
using EastBancTestAssignment.DAL.Interfaces.Repositories;

namespace EastBancTestAssignment.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IItemRepository ItemRepository { get; }
        IBackpackTaskRepository BackpackTaskRepository { get; }
        Task CompleteAsync();
    }
}