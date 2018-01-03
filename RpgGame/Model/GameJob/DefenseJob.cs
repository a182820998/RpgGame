using RpgGame.Model.GameCharacter;

namespace RpgGame.Model.GameJob
{
    class DefenseJob : Job
    {
        public override IDamageBy DamageBy => new DefenseDamageBy();

        public override IAntagonisticBy AntagonisticBy => new DefenseAntagonisticBy();

        internal class DefenseDamageBy : IDamageBy
        {
            public int this[BaseCharacter character] => (character.Job is RemoteJob)
                ? character.Str + character.Job.ExtraStr + 2
                : character.Str + character.Job.ExtraStr;
        }

        internal class DefenseAntagonisticBy : IAntagonisticBy
        {
            public string this[BaseCharacter character] => (character.Job is RemoteJob) ? "*屬性相剋！" : "";
        }
    }
}