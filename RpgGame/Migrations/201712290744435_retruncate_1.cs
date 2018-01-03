namespace RpgGame.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class retruncate_1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserCharacters", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.GameMonsters", "JobId", "dbo.Jobs");
            DropIndex("dbo.UserCharacters", new[] { "JobId" });
            DropIndex("dbo.GameMonsters", new[] { "JobId" });
            DropColumn("dbo.UserCharacters", "JobId");
            DropColumn("dbo.Jobs", "Discriminator");
            DropColumn("dbo.GameMonsters", "JobId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GameMonsters", "JobId", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.UserCharacters", "JobId", c => c.Int(nullable: false));
            CreateIndex("dbo.GameMonsters", "JobId");
            CreateIndex("dbo.UserCharacters", "JobId");
            AddForeignKey("dbo.GameMonsters", "JobId", "dbo.Jobs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserCharacters", "JobId", "dbo.Jobs", "Id", cascadeDelete: true);
        }
    }
}
