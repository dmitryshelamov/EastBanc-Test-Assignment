namespace EastBancTestAssignment.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCombinationCalculatedFromBackpack : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BackpackTasks", "CombinationCalculated");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BackpackTasks", "CombinationCalculated", c => c.Int(nullable: false));
        }
    }
}
