namespace EastBancTestAssignment.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyDomainModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ItemCombinationItems", "ItemCombination_Id", "dbo.ItemCombinations");
            DropForeignKey("dbo.ItemCombinationItems", "Item_Id", "dbo.Items");
            DropForeignKey("dbo.BackpackTaskItems", "BackpackTask_Id", "dbo.BackpackTasks");
            DropForeignKey("dbo.BackpackTaskItems", "Item_Id", "dbo.Items");
            DropForeignKey("dbo.ItemCombinations", "BackpackTask_Id", "dbo.BackpackTasks");
            DropForeignKey("dbo.Items", "BackpackTask_Id", "dbo.BackpackTasks");
            DropIndex("dbo.Items", new[] { "BackpackTask_Id" });
            DropIndex("dbo.ItemCombinations", new[] { "BackpackTask_Id" });
            DropIndex("dbo.ItemCombinationItems", new[] { "ItemCombination_Id" });
            DropIndex("dbo.ItemCombinationItems", new[] { "Item_Id" });
            DropIndex("dbo.BackpackTaskItems", new[] { "BackpackTask_Id" });
            DropIndex("dbo.BackpackTaskItems", new[] { "Item_Id" });
            DropPrimaryKey("dbo.ItemCombinations");
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
                "dbo.BackpackBestItemSets",
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
                "dbo.ItemCombinationSets",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        IsCalculated = c.Boolean(nullable: false),
                        BackpackTaskId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BackpackTasks", t => t.BackpackTaskId, cascadeDelete: true)
                .Index(t => t.BackpackTaskId);
            
            AddColumn("dbo.ItemCombinations", "ItemCombinationSetId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.ItemCombinations", "ItemId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.BackpackTasks", "CombinationCalculated", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ItemCombinations", new[] { "ItemCombinationSetId", "ItemId" });
            CreateIndex("dbo.ItemCombinations", "ItemCombinationSetId");
            CreateIndex("dbo.ItemCombinations", "ItemId");
            AddForeignKey("dbo.ItemCombinations", "ItemId", "dbo.Items", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ItemCombinations", "ItemCombinationSetId", "dbo.ItemCombinationSets", "Id", cascadeDelete: true);
            DropColumn("dbo.BackpackTasks", "NumberOfUniqueItemCombination");
            DropColumn("dbo.Items", "BackpackTask_Id");
            DropColumn("dbo.ItemCombinations", "Id");
            DropColumn("dbo.ItemCombinations", "IsCalculated");
            DropColumn("dbo.ItemCombinations", "BackpackTask_Id");
            DropTable("dbo.ItemCombinationItems");
            DropTable("dbo.BackpackTaskItems");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BackpackTaskItems",
                c => new
                    {
                        BackpackTask_Id = c.String(nullable: false, maxLength: 128),
                        Item_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.BackpackTask_Id, t.Item_Id });
            
            CreateTable(
                "dbo.ItemCombinationItems",
                c => new
                    {
                        ItemCombination_Id = c.String(nullable: false, maxLength: 128),
                        Item_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ItemCombination_Id, t.Item_Id });
            
            AddColumn("dbo.ItemCombinations", "BackpackTask_Id", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.ItemCombinations", "IsCalculated", c => c.Boolean(nullable: false));
            AddColumn("dbo.ItemCombinations", "Id", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Items", "BackpackTask_Id", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.BackpackTasks", "NumberOfUniqueItemCombination", c => c.Double(nullable: false));
            DropForeignKey("dbo.ItemCombinationSets", "BackpackTaskId", "dbo.BackpackTasks");
            DropForeignKey("dbo.ItemCombinations", "ItemCombinationSetId", "dbo.ItemCombinationSets");
            DropForeignKey("dbo.ItemCombinations", "ItemId", "dbo.Items");
            DropForeignKey("dbo.BackpackBestItemSets", "ItemId", "dbo.Items");
            DropForeignKey("dbo.BackpackBestItemSets", "BackpackTaskId", "dbo.BackpackTasks");
            DropForeignKey("dbo.BackpackItems", "ItemId", "dbo.Items");
            DropForeignKey("dbo.BackpackItems", "BackpackTaskId", "dbo.BackpackTasks");
            DropIndex("dbo.ItemCombinations", new[] { "ItemId" });
            DropIndex("dbo.ItemCombinations", new[] { "ItemCombinationSetId" });
            DropIndex("dbo.ItemCombinationSets", new[] { "BackpackTaskId" });
            DropIndex("dbo.BackpackBestItemSets", new[] { "ItemId" });
            DropIndex("dbo.BackpackBestItemSets", new[] { "BackpackTaskId" });
            DropIndex("dbo.BackpackItems", new[] { "ItemId" });
            DropIndex("dbo.BackpackItems", new[] { "BackpackTaskId" });
            DropPrimaryKey("dbo.ItemCombinations");
            AlterColumn("dbo.BackpackTasks", "CombinationCalculated", c => c.Double(nullable: false));
            DropColumn("dbo.ItemCombinations", "ItemId");
            DropColumn("dbo.ItemCombinations", "ItemCombinationSetId");
            DropTable("dbo.ItemCombinationSets");
            DropTable("dbo.BackpackBestItemSets");
            DropTable("dbo.BackpackItems");
            AddPrimaryKey("dbo.ItemCombinations", "Id");
            CreateIndex("dbo.BackpackTaskItems", "Item_Id");
            CreateIndex("dbo.BackpackTaskItems", "BackpackTask_Id");
            CreateIndex("dbo.ItemCombinationItems", "Item_Id");
            CreateIndex("dbo.ItemCombinationItems", "ItemCombination_Id");
            CreateIndex("dbo.ItemCombinations", "BackpackTask_Id");
            CreateIndex("dbo.Items", "BackpackTask_Id");
            AddForeignKey("dbo.Items", "BackpackTask_Id", "dbo.BackpackTasks", "Id");
            AddForeignKey("dbo.ItemCombinations", "BackpackTask_Id", "dbo.BackpackTasks", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BackpackTaskItems", "Item_Id", "dbo.Items", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BackpackTaskItems", "BackpackTask_Id", "dbo.BackpackTasks", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ItemCombinationItems", "Item_Id", "dbo.Items", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ItemCombinationItems", "ItemCombination_Id", "dbo.ItemCombinations", "Id", cascadeDelete: true);
        }
    }
}
