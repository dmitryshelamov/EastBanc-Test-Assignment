using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public async Task<BackpackTask> GetAsync(string id)
        {
            return await _context.BackpackTasks
                .Include(b => b.BackpackItems)
                .Include(b => b.BestItemSet)
                .Include(b => b.ItemCombinationSets)
                .SingleOrDefaultAsync(bt => bt.Id == id);
        }

        public async Task<List<BackpackTask>> GetAllAsync()
        {
            return await _context.BackpackTasks
                .Include(b => b.BackpackItems)
                .Include(b => b.BestItemSet)
                .Include(b => b.ItemCombinationSets)
                .ToListAsync();
        }

        public List<BackpackTask> GetAll()
        {
            return _context.BackpackTasks
                .Include(b => b.BackpackItems)
                .Include(b => b.BestItemSet)
                .Include(b => b.ItemCombinationSets)
                .ToList();
        }

        public async Task Remove(string id)
        {
            var backpack = await _context.BackpackTasks.Include(b => b.ItemCombinationSets).Include(b => b.BestItemSet)
                .Include(b => b.BackpackItems).SingleOrDefaultAsync(t => t.Id == id);
            _context.BackpackTasks.Remove(backpack);
        }
    }
}
