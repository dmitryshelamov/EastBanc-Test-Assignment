using System.Collections.Generic;
using EastBancTestAssignment.KnapsackProblem.DAL.Models;

namespace EastBancTestAssignment.KnapsackProblem.DAL.Interfaces.Repositories
{
    public interface ICombinationSetRepository
    {
        void RemoveRange(IEnumerable<CombinationSet> sets);
    }
}