using System.Data.Entity;
using EastBancTestAssignment.KnapsackProblem.DAL.Models;

namespace EastBancTestAssignment.KnapsackProblem.DAL.Interfaces
{
    public interface IAppDbContext
    {
        IDbSet<BackpackTask> BackpackTasks { get; }
        DbSet<CombinationSet> CombinationSets { get; }
    }
}