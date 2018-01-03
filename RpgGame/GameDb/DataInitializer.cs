using System.Collections.Generic;
using System.Data.Entity;
using RpgGame.Model.GameCharacter;
using RpgGame.Model.GameJob;

namespace RpgGame.GameDb
{
    class DataInitializer : DropCreateDatabaseIfModelChanges<RpgDbContext>
    {
        protected override void Seed(RpgDbContext context)
        {
            var initJobs = new List<Job>
            {
                new MeleeJob {Name = "盜賊", ExtraStr = 2, ExtraDex = 0, ExtraCon = 0, Attribute = "Melee"},
                new MeleeJob {Name = "刺客", ExtraStr = 3, ExtraDex = 0, ExtraCon = 0, Attribute = "Melee"},
                new RemoteJob {Name = "獵人", ExtraStr = 0, ExtraDex = 2, ExtraCon = 0, Attribute = "Remote"},
                new RemoteJob {Name = "神射手", ExtraStr = 0, ExtraDex = 3, ExtraCon = 0, Attribute = "Remote"},
                new DefenseJob {Name = "戰士", ExtraStr = 0, ExtraDex = 0, ExtraCon = 2, Attribute = "Defense"},
                new DefenseJob {Name = "狂戰士", ExtraStr = 0, ExtraDex = 0, ExtraCon = 3, Attribute = "Defense"}
            };

            var initMonsters = new List<GameMonster>
            {
                new GameMonster {Name = "哥布林", Str = 1, Dex = 6, Con = 1, JobId = 1},
                new GameMonster {Name = "史萊姆", Str = 1, Dex = 1, Con = 6, JobId = 1},
                new GameMonster {Name = "獸人", Str = 6, Dex = 1, Con = 1, JobId = 1},
                new GameMonster {Name = "大青蛙", Str = 3, Dex = 2, Con = 3, JobId = 1}
            };

            initJobs.ForEach(job => context.Jobs.Add(job));
            initMonsters.ForEach(monster => context.Monsterses.Add(monster));

            base.Seed(context);
        }
    }
}
