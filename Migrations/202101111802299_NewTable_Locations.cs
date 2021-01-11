namespace DS3Wiki.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewTable_Locations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LocationEnemies",
                c => new
                    {
                        Location_Id = c.Int(nullable: false),
                        Enemy_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Location_Id, t.Enemy_Id })
                .ForeignKey("dbo.Locations", t => t.Location_Id, cascadeDelete: true)
                .ForeignKey("dbo.Enemies", t => t.Enemy_Id, cascadeDelete: true)
                .Index(t => t.Location_Id)
                .Index(t => t.Enemy_Id);
            
            AlterColumn("dbo.Enemies", "Description", c => c.String());
            AlterColumn("dbo.Weapons", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LocationEnemies", "Enemy_Id", "dbo.Enemies");
            DropForeignKey("dbo.LocationEnemies", "Location_Id", "dbo.Locations");
            DropIndex("dbo.LocationEnemies", new[] { "Enemy_Id" });
            DropIndex("dbo.LocationEnemies", new[] { "Location_Id" });
            AlterColumn("dbo.Weapons", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Enemies", "Description", c => c.String(nullable: false));
            DropTable("dbo.LocationEnemies");
            DropTable("dbo.Locations");
        }
    }
}
