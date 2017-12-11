using System.Collections.Generic;
using EastBancTestAssignment.KnapsackProblem.DAL.Interfaces;
using EastBancTestAssignment.KnapsackProblem.DAL.Interfaces.Repositories;
using EastBancTestAssignment.KnapsackProblem.DAL.Models;

namespace EastBancTestAssignment.KnapsackProblem.DAL.Repositories
{
    public class CombinationSetRepository : ICombinationSetRepository
    {
        private readonly IAppDbContext _context;

        public CombinationSetRepository(IAppDbContext context)
        {
            _context = context;
        }

        public void RemoveRange(IEnumerable<CombinationSet> sets)
        {
            _context.CombinationSets.RemoveRange(sets);
        }
    }
}