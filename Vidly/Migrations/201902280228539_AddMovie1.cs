namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMovie1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "DateAdded", c => c.DateTime(nullable: false));
            DropColumn("dbo.Movies", "DateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "DateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Movies", "DateAdded");
        }
    }
}
