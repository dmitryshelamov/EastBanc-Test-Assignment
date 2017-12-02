using EastBancTestAssignment.Core.Models;

namespace EastBancTestAssignment.DAL.Interfaces.Repositories
{
    public interface IItemRepository
    {
        void Add(Item item);
        Item Get(string itemId);
    }
}