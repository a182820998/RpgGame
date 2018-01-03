namespace RpgGame.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cancel_job_identity : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Jobs");
            AlterColumn("dbo.Jobs", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Jobs", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Jobs");
            AlterColumn("dbo.Jobs", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Jobs", "Id");
        }
    }
}
