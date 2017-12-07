using System.Data.Entity.Migrations;

namespace EastBancTestAssignment.KnapsackProblem.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<EastBancTestAssignment.KnapsackProblem.DAL.Repositories.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DAL\Migrations";
        }

        protected override void Seed(EastBancTestAssignment.KnapsackProblem.DAL.Repositories.AppDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
