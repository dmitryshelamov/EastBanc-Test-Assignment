using System.Data.Entity;
using EastBancTestAssignment.KnapsackProblem.Core.Models;

namespace EastBancTestAssignment.KnapsackProblem.DAL.Interfaces
{
    public interface IAppDbContext
    {
        IDbSet<BackpackTask> BackpackTasks { get; }
    }
}