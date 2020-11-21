using System;
using Com.Lepecki.BattleshipGame.Engine.Model;

namespace Com.Lepecki.BattleshipGame.Engine.Data
{
    public record PlaceBattleshipEvent : PlaceShipEvent
    {
        public PlaceBattleshipEvent(Guid gameId, Coordinate stern, Orientation orientation) : base(gameId, 5, stern, orientation)
        {
        }
    }
}
