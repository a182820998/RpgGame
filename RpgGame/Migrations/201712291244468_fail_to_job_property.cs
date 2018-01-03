namespace RpgGame.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fail_to_job_property : DbMigration
    {
        public override void Up()
        {
            Sql("TRUNCATE TABLE GameMonsters");
            Sql("TRUNCATE TABLE UserCharacters");
            Sql("TRUNCATE TABLE Jobs");
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