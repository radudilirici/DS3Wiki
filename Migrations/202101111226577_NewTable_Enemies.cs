namespace DS3Wiki.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewTable_Enemies : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Enemies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WeaponEnemies",
                c => new
                    {
                        Weapon_Id = c.Int(nullable: false),
                        Enemy_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Weapon_Id, t.Enemy_Id })
                .ForeignKey("dbo.Weapons", t => t.Weapon_Id, cascadeDelete: true)
                .ForeignKey("dbo.Enemies", t => t.Enemy_Id, cascadeDelete: true)
                .Index(t => t.Weapon_Id)
                .Index(t => t.Enemy_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WeaponEnemies", "Enemy_Id", "dbo.Enemies");
            DropForeignKey("dbo.WeaponEnemies", "Weapon_Id", "dbo.Weapons");
            DropIndex("dbo.WeaponEnemies", new[] { "Enemy_Id" });
            DropIndex("dbo.WeaponEnemies", new[] { "Weapon_Id" });
            DropTable("dbo.WeaponEnemies");
            DropTable("dbo.Enemies");
        }
    }
}
