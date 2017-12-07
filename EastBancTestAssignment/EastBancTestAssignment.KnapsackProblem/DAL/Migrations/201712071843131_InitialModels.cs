using System.Data.Entity.Migrations;

namespace EastBancTestAssignment.KnapsackProblem.DAL.Migrations
{
    public partial class InitialModels : DbMigration
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
                        StartTime = c.DateTime(),
                        EndTime = c.DateTime(),
                        BestItemSetPrice = c.Int(nullable: false),
                        BestItemSetWeight = c.Int(nullable: false),
                        Complete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BackpackItems",
                c => new
                    {
                        BackpackTaskId = c.String(nullable: false, maxLength: 128),
                        ItemId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.BackpackTaskId, t.ItemId })
                .ForeignKey("dbo.BackpackTasks", t => t.BackpackTaskId, cascadeDelete: true)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.BackpackTaskId)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Weight = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BestItemSets",
                c => new
                    {
                        BackpackTaskId = c.String(nullable: false, maxLength: 128),
                        ItemId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.BackpackTaskId, t.ItemId })
                .ForeignKey("dbo.BackpackTasks", t => t.BackpackTaskId, cascadeDelete: true)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.BackpackTaskId)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.CombinationSets",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        IsCalculated = c.Boolean(nullable: false),
                        BackpackTaskId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BackpackTasks", t => t.BackpackTaskId, cascadeDelete: true)
                .Index(t => t.BackpackTaskId);
            
            CreateTable(
                "dbo.ItemCombinations",
                c => new
                    {
                        CombinationSetId = c.String(nullable: false, maxLength: 128),
                        ItemId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.CombinationSetId, t.ItemId })
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.CombinationSets", t => t.CombinationSetId, cascadeDelete: true)
                .Index(t => t.CombinationSetId)
                .Index(t => t.ItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CombinationSets", "BackpackTaskId", "dbo.BackpackTasks");
            DropForeignKey("dbo.ItemCombinations", "CombinationSetId", "dbo.CombinationSets");
            DropForeignKey("dbo.ItemCombinations", "ItemId", "dbo.Items");
            DropForeignKey("dbo.BestItemSets", "ItemId", "dbo.Items");
            DropForeignKey("dbo.BestItemSets", "BackpackTaskId", "dbo.BackpackTasks");
            DropForeignKey("dbo.BackpackItems", "ItemId", "dbo.Items");
            DropForeignKey("dbo.BackpackItems", "BackpackTaskId", "dbo.BackpackTasks");
            DropIndex("dbo.ItemCombinations", new[] { "ItemId" });
            DropIndex("dbo.ItemCombinations", new[] { "CombinationSetId" });
            DropIndex("dbo.CombinationSets", new[] { "BackpackTaskId" });
            DropIndex("dbo.BestItemSets", new[] { "ItemId" });
            DropIndex("dbo.BestItemSets", new[] { "BackpackTaskId" });
            DropIndex("dbo.BackpackItems", new[] { "ItemId" });
            DropIndex("dbo.BackpackItems", new[] { "BackpackTaskId" });
            DropTable("dbo.ItemCombinations");
            DropTable("dbo.CombinationSets");
            DropTable("dbo.BestItemSets");
            DropTable("dbo.Items");
            DropTable("dbo.BackpackItems");
            DropTable("dbo.BackpackTasks");
        }
    }
}
