using System.Threading.Tasks;
using EastBancTestAssignment.DAL.Interfaces;
using EastBancTestAssignment.DAL.Interfaces.Repositories;
using EastBancTestAssignment.DAL.Repositories;

namespace EastBancTestAssignment.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IItemRepository ItemRepository { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            ItemRepository = new ItemRepository(context);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
