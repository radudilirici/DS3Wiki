namespace DS3Wiki.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Make_Weapons_Description_Required : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Weapons", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Weapons", "Description", c => c.String());
        }
    }
}
