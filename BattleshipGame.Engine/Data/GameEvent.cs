using System;

namespace Com.Lepecki.BattleshipGame.Engine.Data
{
    public abstract record GameEvent
    {
        public Guid GameId { get; init; }

        public Guid PlayerId { get; init; }
    }
}
