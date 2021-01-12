namespace DS3Wiki.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class NewTable_Comments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Category = c.String(nullable: false),
                    Email = c.String(nullable: false),
                    Text = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropTable("dbo.Comments");
        }
    }
}
