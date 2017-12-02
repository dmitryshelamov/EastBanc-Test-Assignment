using EastBancTestAssignment.Core.Models;

namespace EastBancTestAssignment.DAL.Interfaces.Repositories
{
    public interface IItemRepository
    {
        void Add(Item item);
        void Get(string itemId);
    }
}