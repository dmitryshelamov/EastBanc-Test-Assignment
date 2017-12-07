using System.Data.Entity;
using EastBancTestAssignment.KnapsackProblem.Core.Models;
using EastBancTestAssignment.KnapsackProblem.DAL.Interfaces;

namespace EastBancTestAssignment.KnapsackProblem.DAL.Repositories
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public IDbSet<BackpackTask> BackpackTasks { get; set; }

        public AppDbContext() : base("DefaultConnection") { }

        public AppDbContext(string conectionString) : base(conectionString) { }
    }
}