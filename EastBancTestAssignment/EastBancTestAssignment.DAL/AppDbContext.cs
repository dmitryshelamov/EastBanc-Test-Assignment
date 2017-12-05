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
            modelBuilder.Entity<BackpackBestItemSet>()
                .HasKey(i => new { i.BackpackTaskId, i.ItemId });

            modelBuilder.Entity<BackpackBestItemSet>()
                .HasRequired(i => i.BackpackTask)
                .WithMany(i => i.BestItemSet)
                .HasForeignKey(i => i.BackpackTaskId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ItemCombination>()
                .HasKey(i => new { i.ItemCombinationSetId, i.ItemId });

            modelBuilder.Entity<ItemCombinationSet>()
                .HasMany(i => i.ItemCombinations)
                .WithRequired(i => i.ItemCombinationSet)
                .HasForeignKey(i => i.ItemCombinationSetId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<BackpackTask>()
                .HasMany(b => b.ItemCombinationSets)
                .WithRequired(c => c.BackpackTask)
                .HasForeignKey(c => c.BackpackTaskId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<BackpackTask>()
                .HasMany(b => b.BackpackItems)
                .WithRequired(c => c.BackpackTask)
                .HasForeignKey(c => c.BackpackTaskId)
                .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }
    }
}
