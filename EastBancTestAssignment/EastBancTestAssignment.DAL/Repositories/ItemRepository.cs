using System.Linq;
using EastBancTestAssignment.Core.Models;
using EastBancTestAssignment.DAL.Interfaces;
using EastBancTestAssignment.DAL.Interfaces.Repositories;

namespace EastBancTestAssignment.DAL.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly IAppDbContext _context;

        public ItemRepository(IAppDbContext context)
        {
            _context = context;
        }

        public void Add(Item item)
        {
            _context.Items.Add(item);
        }

        public Item Get(string itemId)
        {
            return _context.Items.SingleOrDefault(i => i.Id == itemId);
        }
    }
}
