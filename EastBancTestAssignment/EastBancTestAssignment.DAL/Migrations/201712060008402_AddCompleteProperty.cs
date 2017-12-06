namespace EastBancTestAssignment.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompleteProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BackpackTasks", "Complete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BackpackTasks", "Complete");
        }
    }
}
