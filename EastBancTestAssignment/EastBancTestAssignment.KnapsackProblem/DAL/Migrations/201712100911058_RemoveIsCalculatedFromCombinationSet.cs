namespace EastBancTestAssignment.KnapsackProblem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveIsCalculatedFromCombinationSet : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CombinationSets", "IsCalculated");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CombinationSets", "IsCalculated", c => c.Boolean(nullable: false));
        }
    }
}
