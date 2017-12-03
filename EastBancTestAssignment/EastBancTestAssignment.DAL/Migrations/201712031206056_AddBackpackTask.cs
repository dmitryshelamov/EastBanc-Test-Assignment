namespace EastBancTestAssignment.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBackpackTask : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BackpackTasks",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        WeightLimit = c.Int(nullable: false),
                        BackpackTaskSolution_BestItemSetPrice = c.Int(nullable: false),
                        BackpackTaskSolution_BestItemSetWeight = c.Int(nullable: false),
                        BackpackTaskSolution_StartTime = c.DateTime(),
                        BackpackTaskSolution_EndTime = c.DateTime(),
                        BackpackTaskSolution_NumberOfUniqueItemCombination = c.Double(nullable: false),
                        BackpackTaskSolution_CombinationCalculated = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Items", "BackpackTask_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Items", "BackpackTask_Id");
            AddForeignKey("dbo.Items", "BackpackTask_Id", "dbo.BackpackTasks", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "BackpackTask_Id", "dbo.BackpackTasks");
            DropIndex("dbo.Items", new[] { "BackpackTask_Id" });
            DropColumn("dbo.Items", "BackpackTask_Id");
            DropTable("dbo.BackpackTasks");
        }
    }
}
