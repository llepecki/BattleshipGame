namespace Com.Lepecki.BattleshipGame.Engine.Model
{
    public class ShipField : Field
    {
        private readonly Ship _ship;

        public ShipField(Ship ship)
        {
            _ship = ship;
        }

        public override Occupant GetOwnerView()
        {
            return _ship.Class;
        }

        public override Occupant GetOpponentView()
        {
            if (_ship.Sunken)
            {
                return _ship.Class;
            }

            if (WasFiredAt)
            {
                return Occupant.Hit;
            }

            return Occupant.Hidden;
        }

        public override void Hit()
        {
            base.Hit();
            _ship.Hit();
        }
    }
}
