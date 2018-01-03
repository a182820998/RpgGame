namespace RpgGame.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test_job_as_char_property : DbMigration
    {
        public override void Up()
        {
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GameMonsters", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.UserCharacters", "JobId", "dbo.Jobs");
            DropIndex("dbo.GameMonsters", new[] { "JobId" });
            DropIndex("dbo.UserCharacters", new[] { "JobId" });
        }
    }
}