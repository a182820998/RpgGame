using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using RpgGame.Model.GameCharacter;
using RpgGame.Model.GameJob;

namespace RpgGame.GameDb
{
    //connectionString: Remember! the connectionString's name have to equals context's name!
    //<add name = "RpgDbContext"
    //connectionString="
    //data source=(LocalDb)\MSSQLLocalDB;
    //AttachDbFilename=C:\Users\user\Documents\Visual_Studio_2017\Projects\netFramework461\RpgGame\RpgGame\App_Data\RpgGameDb.mdf;
    //Initial Catalog=RpgGameDb;
    //integrated security=True;
    //MultipleActiveResultSets=True;
    //App=EntityFramework" providerName="System.Data.SqlClient" />
    class RpgDbContext : DbContext
    {
        public RpgDbContext() : base("RpgDbContext")
        {
            Database.SetInitializer(new DataInitializer());
        }

        public DbSet<UserCharacter> Characters { get; set; }
        public DbSet<GameMonster> Monsterses { get; set; }
        public DbSet<Job> Jobs { get; set; }
    }
}