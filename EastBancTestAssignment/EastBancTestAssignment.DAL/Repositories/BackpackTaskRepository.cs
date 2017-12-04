using System.Data.Entity;
using System.Threading.Tasks;
using EastBancTestAssignment.Core.Models;
using EastBancTestAssignment.DAL.Interfaces;

namespace EastBancTestAssignment.DAL.Repositories
{
    public class BackpackTaskRepository
    {
        private readonly IAppDbContext _context;

        public BackpackTaskRepository(IAppDbContext context)
        {
            _context = context;
        }

        public void Add(BackpackTask backpackTask)
        {
            _context.BackpackTasks.Add(backpackTask);
        }

        public async Task<BackpackTask> Get(string backpackTaskId)
        {
            return await _context.BackpackTasks.SingleOrDefaultAsync(b => b.Id == backpackTaskId);
        }
    }
}
