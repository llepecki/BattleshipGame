using System;
using Com.Lepecki.BattleshipGame.Engine.Model;

namespace Com.Lepecki.BattleshipGame.Engine.Data
{
    public abstract record PlaceShipEvent : GameEvent
    {
        protected PlaceShipEvent(Guid gameId, int length, Coordinate stern, Orientation orientation) : base(gameId)
        {
            Length = length;
            Stern = stern;
            Orientation = orientation;
        }

        public int Length { get; }

        public Coordinate Stern { get; }

        public Orientation Orientation { get; }
    }
}

