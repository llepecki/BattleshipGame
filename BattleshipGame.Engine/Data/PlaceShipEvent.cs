using Com.Lepecki.BattleshipGame.Engine.Model;

namespace Com.Lepecki.BattleshipGame.Engine.Data
{
    public abstract record PlaceShipEvent : GameEvent
    {
        public Coordinate Stern { get; init; }

        public Orientation Orientation { get; init; }
    }
}

