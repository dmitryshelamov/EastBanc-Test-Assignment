using System.Data.Entity;
using EastBancTestAssignment.Core.Models;
using EastBancTestAssignment.DAL.Interfaces;

namespace EastBancTestAssignment.DAL
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public IDbSet<Item> Items { get; set; }
        public IDbSet<BackpackTask> BackpackTasks { get; set; }

        public AppDbContext() : base("DefaultConnection") { }

        public AppDbContext(string conectionString) : base(conectionString) { }
    }
}
