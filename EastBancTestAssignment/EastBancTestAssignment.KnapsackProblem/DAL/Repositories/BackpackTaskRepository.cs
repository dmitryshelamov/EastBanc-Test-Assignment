using EastBancTestAssignment.KnapsackProblem.Core.Models;
using EastBancTestAssignment.KnapsackProblem.DAL.Interfaces;
using EastBancTestAssignment.KnapsackProblem.DAL.Interfaces.Repositories;

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
    }
}