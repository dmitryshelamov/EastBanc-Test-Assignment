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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BackpackTask>()
                .HasMany(b => b.Items)
                .WithRequired(i => i.BackpackTask)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BackpackTask>()
                .HasMany(b => b.BestItemsSet)
                .WithMany(i => i.BestPrice);

            modelBuilder.Entity<BackpackTask>()
                .HasMany(b => b.ItemCombinations)
                .WithRequired(c => c.BackpackTask);

            modelBuilder.Entity<ItemCombination>()
                .HasMany(c => c.Items)
                .WithMany(i => i.ItemCombinations);

            base.OnModelCreating(modelBuilder);
        }
    }
}
