using RpgGame.Model.GameCharacter;

namespace RpgGame.Model.GameJob
{
    class MeleeJob : Job
    {
        public override IDamageBy DamageBy => new MeleeDamageBy();

        public override IAntagonisticBy AntagonisticBy => new MeleeAntagonisticBy();

        internal class MeleeDamageBy : IDamageBy
        {
            public int this[BaseCharacter character] => (character.Job is DefenseJob)
                ? character.Str + character.Job.ExtraStr + 2
                : character.Str + character.Job.ExtraStr;
        }

        internal class MeleeAntagonisticBy : IAntagonisticBy
        {
            public string this[BaseCharacter character] => (character.Job is DefenseJob) ? "*屬性相剋！" : "";
        }
    }
}
