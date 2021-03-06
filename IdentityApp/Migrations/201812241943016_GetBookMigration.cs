namespace IdentityApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GetBookMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Author", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Author", c => c.Int(nullable: false));
        }
    }
}
