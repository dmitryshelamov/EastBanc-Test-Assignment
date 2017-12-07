namespace EastBancTestAssignment.KnapsackProblem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveBackpackItem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BackpackItems", "ItemId", "dbo.Items");
            DropIndex("dbo.BackpackItems", new[] { "BackpackTaskId" });
            DropIndex("dbo.BackpackItems", new[] { "ItemId" });
            AddColumn("dbo.Items", "BackpackTaskId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Items", "BackpackTaskId");
            DropTable("dbo.BackpackItems");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BackpackItems",
                c => new
                    {
                        BackpackTaskId = c.String(nullable: false, maxLength: 128),
                        ItemId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.BackpackTaskId, t.ItemId });
            
            DropIndex("dbo.Items", new[] { "BackpackTaskId" });
            DropColumn("dbo.Items", "BackpackTaskId");
            CreateIndex("dbo.BackpackItems", "ItemId");
            CreateIndex("dbo.BackpackItems", "BackpackTaskId");
            AddForeignKey("dbo.BackpackItems", "ItemId", "dbo.Items", "Id", cascadeDelete: true);
        }
    }
}
