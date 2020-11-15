namespace Com.Lepecki.BattleshipGame.Engine.Model
{
    public class Battleship : Ship
    {
        public Battleship() : base(5)
        {
        }

        public override Occupant Class => Occupant.Battleship;
    }
}
