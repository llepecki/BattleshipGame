using System;
using Com.Lepecki.BattleshipGame.Engine.Model;

namespace Com.Lepecki.BattleshipGame.Engine.Data
{
    public record PlaceDestroyerEvent : PlaceShipEvent
    {
        public PlaceDestroyerEvent(Guid gameId, Coordinate stern, Orientation orientation) : base(gameId, 4, stern, orientation)
        {
        }
    }
}
