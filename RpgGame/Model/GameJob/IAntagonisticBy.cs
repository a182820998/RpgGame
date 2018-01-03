using RpgGame.Model.GameCharacter;

namespace RpgGame.Model.GameJob
{
    public interface IAntagonisticBy
    {
        string this[BaseCharacter character] { get; }
    }
}