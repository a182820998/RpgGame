namespace RpgGame.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class redesign_table : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserCharacters", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.GameMonsters", "JobId", "dbo.Jobs");
            DropIndex("dbo.UserCharacters", new[] { "JobId" });
            DropIndex("dbo.GameMonsters", new[] { "JobId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.GameMonsters", "JobId");
            CreateIndex("dbo.UserCharacters", "JobId");
            AddForeignKey("dbo.GameMonsters", "JobId", "dbo.Jobs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserCharacters", "JobId", "dbo.Jobs", "Id", cascadeDelete: true);
        }
    }
}
