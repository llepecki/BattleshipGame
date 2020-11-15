using Com.Lepecki.BattleshipGame.Engine.Model;

namespace Com.Lepecki.BattleshipGame.Engine.Data
{
    public record FireEvent : GameEvent
    {
        public Coordinate Target { get; init; }
    }
}
