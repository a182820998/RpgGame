using RpgGame.Model.GameCharacter;

namespace RpgGame.Model.GameJob
{
    class RemoteJob : Job
    {
        public override IDamageBy DamageBy => new RemoteDamageBy();

        public override IAntagonisticBy AntagonisticBy => new RemoteAntagonisticBy();

        internal class RemoteDamageBy : IDamageBy
        {
            public int this[BaseCharacter character] => (character.Job is MeleeJob)
                ? character.Str + character.Job.ExtraStr + 2
                : character.Str + character.Job.ExtraStr;
        }

        internal class RemoteAntagonisticBy : IAntagonisticBy
        {
            public string this[BaseCharacter character] => (character.Job is MeleeJob) ? "*屬性相剋！" : "";
        }
    }
}
