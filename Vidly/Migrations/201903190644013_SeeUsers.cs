namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeeUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'a37a19a1-9f56-4fe4-97aa-f89e19445a0c', N'guest@vidly.com', 0, N'AF2nNX0EV0DxGxI92vot56H2nQdB3BaHX8J3wKT+M2FldcmTx+qjyxnjfUgRpmXe9Q==', N'ab605cc6-674a-43ef-91f1-077d28c5e7de', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'f33bce76-b9f6-4ca4-9efd-281bd0084a95', N'admin@vidly.com', 0, N'AEVvWT3sbWK9LYCQxvxoNGVvOsB703rBJ6dwKNWWEUO/rQJ2Y8N9tzH115NcUBmyig==', N'c2764019-1bee-49db-807a-f204c1e3f3d9', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'be594470-c9c1-41c1-8e31-5fb2ed254824', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f33bce76-b9f6-4ca4-9efd-281bd0084a95', N'be594470-c9c1-41c1-8e31-5fb2ed254824')

");
        }
        
        public override void Down()
        {
        }
    }
}
