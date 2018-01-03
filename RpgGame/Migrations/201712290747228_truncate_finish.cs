namespace RpgGame.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class truncate_finish : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserCharacters", "JobId", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.GameMonsters", "JobId", c => c.Int(nullable: false));
            CreateIndex("dbo.UserCharacters", "JobId");
            CreateIndex("dbo.GameMonsters", "JobId");
            AddForeignKey("dbo.UserCharacters", "JobId", "dbo.Jobs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GameMonsters", "JobId", "dbo.Jobs", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GameMonsters", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.UserCharacters", "JobId", "dbo.Jobs");
            DropIndex("dbo.GameMonsters", new[] { "JobId" });
            DropIndex("dbo.UserCharacters", new[] { "JobId" });
            DropColumn("dbo.GameMonsters", "JobId");
            DropColumn("dbo.Jobs", "Discriminator");
            DropColumn("dbo.UserCharacters", "JobId");
        }
    }
}
