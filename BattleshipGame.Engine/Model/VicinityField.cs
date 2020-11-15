using Com.Lepecki.BattleshipGame.Engine.Exceptions;

namespace Com.Lepecki.BattleshipGame.Engine.Model
{
    public class VicinityField : Field
    {
        private readonly Ship _ship;

        public VicinityField(Ship ship)
        {
            _ship = ship;
        }

        public override Occupant GetOwnerView()
        {
            return Occupant.Empty;
        }

        public override Occupant GetOpponentView()
        {
            if (_ship.Sunked)
            {
                return Occupant.Empty;
            }

            if (WasFiredAt)
            {
                return Occupant.Missed;
            }

            return Occupant.Hidden;
        }

        public override void Fire()
        {
            if (_ship.Sunked)
            {
                throw new RuleViolationException("Can't fire at a field next to a sunked ship");
            }

            base.Fire();
        }
    }
}
