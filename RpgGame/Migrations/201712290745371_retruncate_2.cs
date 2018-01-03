namespace RpgGame.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class retruncate_2 : DbMigration
    {
        public override void Up()
        {
            Sql("TRUNCATE TABLE Jobs");
        }
        
        public override void Down()
        {
        }
    }
}
