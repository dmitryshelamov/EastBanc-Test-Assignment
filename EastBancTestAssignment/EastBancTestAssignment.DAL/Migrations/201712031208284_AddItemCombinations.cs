namespace EastBancTestAssignment.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddItemCombinations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ItemCombinations",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        IsCalculated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Items", "ItemCombination_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Items", "ItemCombination_Id");
            AddForeignKey("dbo.Items", "ItemCombination_Id", "dbo.ItemCombinations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "ItemCombination_Id", "dbo.ItemCombinations");
            DropIndex("dbo.Items", new[] { "ItemCombination_Id" });
            DropColumn("dbo.Items", "ItemCombination_Id");
            DropTable("dbo.ItemCombinations");
        }
    }
}
