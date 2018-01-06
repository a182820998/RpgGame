namespace RpgGame.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserCharacters",
                c => new
                    {
                        CharacterId = c.Int(nullable: false, identity: true),
                        JobId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Level = c.Int(nullable: false),
                        Str = c.Int(nullable: false),
                        Dex = c.Int(nullable: false),
                        Con = c.Int(nullable: false),
                        Exp = c.Int(nullable: false),
                        TotalFightTimes = c.Int(nullable: false),
                        FailFightTimes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CharacterId);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ExtraStr = c.Int(nullable: false),
                        ExtraDex = c.Int(nullable: false),
                        ExtraCon = c.Int(nullable: false),
                        Attribute = c.String(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GameMonsters",
                c => new
                    {
                        CharacterId = c.Int(nullable: false, identity: true),
                        JobId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Level = c.Int(nullable: false),
                        Str = c.Int(nullable: false),
                        Dex = c.Int(nullable: false),
                        Con = c.Int(nullable: false),
                        Exp = c.Int(nullable: false),
                        TotalFightTimes = c.Int(nullable: false),
                        FailFightTimes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CharacterId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GameMonsters");
            DropTable("dbo.Jobs");
            DropTable("dbo.UserCharacters");
        }
    }
}
