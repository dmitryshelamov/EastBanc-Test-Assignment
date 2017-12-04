using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using EastBancTestAssignment.Core.Models;
using EastBancTestAssignment.DAL.Interfaces;
using EastBancTestAssignment.DAL.Interfaces.Repositories;

namespace EastBancTestAssignment.DAL.Repositories
{
    public class BackpackTaskRepository : IBackpackTaskRepository
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

        public async Task<BackpackTask> Get(string id)
        {
            return await _context.BackpackTasks
                .Include(b => b.Items)
                .Include(b => b.BestItemsSet)
                .Include(b => b.ItemCombinations)
                .SingleOrDefaultAsync(bt => bt.Id == id);
        }

        public async Task<List<BackpackTask>> GetAll()
        {
            return await _context.BackpackTasks
                .Include(b => b.Items)
                .Include(b => b.BestItemsSet)
                .Include(b => b.ItemCombinations)
                .ToListAsync();
        }
    }
}
