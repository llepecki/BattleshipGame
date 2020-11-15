using Com.Lepecki.BattleshipGame.Engine.Exceptions;

namespace Com.Lepecki.BattleshipGame.Engine.Model
{
    public abstract class Ship
    {
        protected Ship(int length)
        {
            Length = length;
        }

        public abstract Occupant Class { get; }

        public int Length { get; }

        public int Hits { get; private set; }

        public bool Sunked => Hits == Length;

        public void Hit()
        {
            if (Sunked)
            {
                throw new RuleViolationException("Can't hit a sunked ship");
            }

            Hits++;
        }
    }
}
