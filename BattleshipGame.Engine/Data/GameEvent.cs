using System;

namespace Com.Lepecki.BattleshipGame.Engine.Data
{
    public abstract record GameEvent
    {
        protected GameEvent(Guid gameId)
        {
            GameId = gameId;
        }

        public Guid GameId { get; }
    }
}
