namespace EastBancTestAssignment.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
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
                        BestItemSetPrice = c.Int(nullable: false),
                        BestItemSetWeight = c.Int(nullable: false),
                        StartTime = c.DateTime(),
                        EndTime = c.DateTime(),
                        NumberOfUniqueItemCombination = c.Double(nullable: false),
                        CombinationCalculated = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Weight = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        BackpackTask_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BackpackTasks", t => t.BackpackTask_Id)
                .Index(t => t.BackpackTask_Id);
            
            CreateTable(
                "dbo.ItemCombinations",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        IsCalculated = c.Boolean(nullable: false),
                        BackpackTask_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BackpackTasks", t => t.BackpackTask_Id, cascadeDelete: true)
                .Index(t => t.BackpackTask_Id);
            
            CreateTable(
                "dbo.ItemCombinationItems",
                c => new
                    {
                        ItemCombination_Id = c.String(nullable: false, maxLength: 128),
                        Item_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ItemCombination_Id, t.Item_Id })
                .ForeignKey("dbo.ItemCombinations", t => t.ItemCombination_Id, cascadeDelete: true)
                .ForeignKey("dbo.Items", t => t.Item_Id, cascadeDelete: true)
                .Index(t => t.ItemCombination_Id)
                .Index(t => t.Item_Id);
            
            CreateTable(
                "dbo.BackpackTaskItems",
                c => new
                    {
                        BackpackTask_Id = c.String(nullable: false, maxLength: 128),
                        Item_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.BackpackTask_Id, t.Item_Id })
                .ForeignKey("dbo.BackpackTasks", t => t.BackpackTask_Id, cascadeDelete: true)
                .ForeignKey("dbo.Items", t => t.Item_Id, cascadeDelete: true)
                .Index(t => t.BackpackTask_Id)
                .Index(t => t.Item_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "BackpackTask_Id", "dbo.BackpackTasks");
            DropForeignKey("dbo.ItemCombinations", "BackpackTask_Id", "dbo.BackpackTasks");
            DropForeignKey("dbo.BackpackTaskItems", "Item_Id", "dbo.Items");
            DropForeignKey("dbo.BackpackTaskItems", "BackpackTask_Id", "dbo.BackpackTasks");
            DropForeignKey("dbo.ItemCombinationItems", "Item_Id", "dbo.Items");
            DropForeignKey("dbo.ItemCombinationItems", "ItemCombination_Id", "dbo.ItemCombinations");
            DropIndex("dbo.BackpackTaskItems", new[] { "Item_Id" });
            DropIndex("dbo.BackpackTaskItems", new[] { "BackpackTask_Id" });
            DropIndex("dbo.ItemCombinationItems", new[] { "Item_Id" });
            DropIndex("dbo.ItemCombinationItems", new[] { "ItemCombination_Id" });
            DropIndex("dbo.ItemCombinations", new[] { "BackpackTask_Id" });
            DropIndex("dbo.Items", new[] { "BackpackTask_Id" });
            DropTable("dbo.BackpackTaskItems");
            DropTable("dbo.ItemCombinationItems");
            DropTable("dbo.ItemCombinations");
            DropTable("dbo.Items");
            DropTable("dbo.BackpackTasks");
        }
    }
}
