namespace Com.Lepecki.BattleshipGame.Engine.Model
{
    public class Destroyer : Ship
    {
        public Destroyer() : base(4)
        {
        }

        public override Occupant Class => Occupant.Destroyer;
    }
}
