using Com.Lepecki.BattleshipGame.Engine.Exceptions;

namespace Com.Lepecki.BattleshipGame.Engine.Model
{
    public abstract class Field
    {
        protected bool WasFiredAt;

        public abstract Occupant GetOwnerView();

        public abstract Occupant GetOpponentView();

        public virtual void Fire()
        {
            if (WasFiredAt)
            {
                throw new RuleViolationException("Forbidden to fire at the same field twice");
            }

            WasFiredAt = true;
        }
    }
}
