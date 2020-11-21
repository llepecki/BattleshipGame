using System;
using Com.Lepecki.BattleshipGame.Engine.Model;

namespace Com.Lepecki.BattleshipGame.Engine.Data
{
    public record FireEvent : GameEvent
    {
        public FireEvent(Guid gameId, Coordinate target) : base(gameId)
        {
            Target = target;
        }

        public Coordinate Target { get; }
    }
}
