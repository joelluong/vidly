namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateNewGernes_1 : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT Genres ON");
            Sql("INSERT INTO Genres (Id, Name) VALUES (6, 'Horror')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (7, 'Musical/Dance')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (8, 'Science Fiction')");
            Sql("SET IDENTITY_INSERT Genres OFF");

        }
        
        public override void Down()
        {
        }
    }
}
