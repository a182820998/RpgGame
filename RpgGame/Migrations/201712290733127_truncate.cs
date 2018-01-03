using System.Data.Entity;

namespace RpgGame.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class truncate : DbMigration
    {
        public override void Up()
        {
            Sql("TRUNCATE TABLE GameMonsters");
            Sql("TRUNCATE TABLE UserCharacters");
        }
        
        public override void Down()
        {
        }
    }
}
