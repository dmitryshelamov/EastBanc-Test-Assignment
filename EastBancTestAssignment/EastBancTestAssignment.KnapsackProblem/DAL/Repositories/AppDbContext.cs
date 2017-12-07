using System.Data.Entity;
using EastBancTestAssignment.KnapsackProblem.DAL.Interfaces;
using EastBancTestAssignment.KnapsackProblem.DAL.Models;

namespace EastBancTestAssignment.KnapsackProblem.DAL.Repositories
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public IDbSet<BackpackTask> BackpackTasks { get; set; }

        public AppDbContext() : base("DefaultConnection") { }

        public AppDbContext(string conectionString) : base(conectionString) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BestItemSet>()
                .HasKey(i => new { i.BackpackTaskId, i.ItemId });

            modelBuilder.Entity<BestItemSet>()
                .HasRequired(i => i.BackpackTask)
                .WithMany(i => i.BestItemSet)
                .HasForeignKey(i => i.BackpackTaskId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ItemCombination>()
                .HasKey(i => new { i.CombinationSetId, i.ItemId });

            modelBuilder.Entity<CombinationSet>()
                .HasMany(i => i.ItemCombinations)
                .WithRequired(i => i.CombinationSet)
                .HasForeignKey(i => i.CombinationSetId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<BackpackTask>()
                .HasMany(b => b.CombinationSets)
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