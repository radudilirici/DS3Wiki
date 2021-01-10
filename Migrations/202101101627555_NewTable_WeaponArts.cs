namespace DS3Wiki.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewTable_WeaponArts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WeaponArts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Weapons", "WeaponArtId", c => c.Int(nullable: false));
            CreateIndex("dbo.Weapons", "WeaponArtId");
            AddForeignKey("dbo.Weapons", "WeaponArtId", "dbo.WeaponArts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Weapons", "WeaponArtId", "dbo.WeaponArts");
            DropIndex("dbo.Weapons", new[] { "WeaponArtId" });
            DropColumn("dbo.Weapons", "WeaponArtId");
            DropTable("dbo.WeaponArts");
        }
    }
}
