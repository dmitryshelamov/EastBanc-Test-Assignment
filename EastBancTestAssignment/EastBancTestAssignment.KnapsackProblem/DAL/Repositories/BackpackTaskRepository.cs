using System.Data.Entity;
using System.Linq;
using EastBancTestAssignment.KnapsackProblem.DAL.Interfaces;
using EastBancTestAssignment.KnapsackProblem.DAL.Interfaces.Repositories;
using EastBancTestAssignment.KnapsackProblem.DAL.Models;

namespace EastBancTestAssignment.KnapsackProblem.DAL.Repositories
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

        public BackpackTask Get(string id)
        {
            return _context.BackpackTasks
                .Include(b => b.BackpackItems)
                .Include(b => b.BestItemSet)
                .Include(b => b.CombinationSets)
                .SingleOrDefault(bt => bt.Id == id);
        }
    }
}