using System.Collections.Generic;
using System.Linq;
using Com.Lepecki.BattleshipGame.Engine.Exceptions;

namespace Com.Lepecki.BattleshipGame.Engine.Model
{
    public class VicinityField : Field
    {
        private readonly Ship[] _ships;

        public VicinityField(Ship ship)
        {
            _ships = new[] { ship };
        }

        public VicinityField(IEnumerable<Ship> ships)
        {
            _ships = ships.ToArray();
        }

        public override Occupant GetOwnerView()
        {
            return Occupant.Empty;
        }

        public override Occupant GetOpponentView()
        {
            if (_ships.Any(ship => ship.Sunken))
            {
                return Occupant.Empty;
            }

            if (WasFiredAt)
            {
                return Occupant.Missed;
            }

            return Occupant.Hidden;
        }

        public override void Hit()
        {
            if (_ships.Any(ship => ship.Sunken))
            {
                throw new RuleViolationException("Forbidden to fire at a field next to a sunken ship");
            }

            base.Hit();
        }

        public VicinityField Merge(VicinityField other)
        {
            return new VicinityField(Enumerable.Concat(_ships, other._ships));
        }
    }
}
