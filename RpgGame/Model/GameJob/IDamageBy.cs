using RpgGame.Model.GameCharacter;

namespace RpgGame.Model.GameJob
{
    public interface IDamageBy
    {
        int this[BaseCharacter character] { get; }
    }
}