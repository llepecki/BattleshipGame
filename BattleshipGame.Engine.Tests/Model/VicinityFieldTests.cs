using Com.Lepecki.BattleshipGame.Engine.Exceptions;
using Com.Lepecki.BattleshipGame.Engine.Model;
using Xunit;

namespace Com.Lepecki.BattleshipGame.Engine.Tests.Model
{
    public class VicinityFieldTests
    {
        [Fact]
        public void NotFiredAtOwnerView()
        {
            Ship ship = new Destroyer();
            Field field = new VicinityField(ship);

            Assert.Equal(Occupant.Empty, field.GetOwnerView());
        }

        [Fact]
        public void NotFiredAtOpponentView()
        {
            Ship ship = new Destroyer();
            Field field = new VicinityField(ship);

            Assert.Equal(Occupant.Hidden, field.GetOpponentView());
        }

        [Fact]
        public void FiredAtNotSunkenOwnerView()
        {
            Ship ship = new Destroyer();
            Field field = new VicinityField(ship);

            field.Hit();

            Assert.Equal(Occupant.Empty, field.GetOwnerView());
        }

        [Fact]
        public void FiredAtNotSunkenOpponentView()
        {
            Ship ship = new Destroyer();
            Field field = new VicinityField(ship);

            field.Hit();

            Assert.Equal(Occupant.Missed, field.GetOpponentView());
        }

        [Fact]
        public void FiredAtSunkenOwnerView()
        {
            Ship ship = new Destroyer();

            while (!ship.Sunken)
            {
                ship.Hit();
            }

            Field field = new VicinityField(ship);

            Assert.Equal(Occupant.Empty, field.GetOwnerView());
        }

        [Fact]
        public void FiredAtSunkenOpponentView()
        {
            Ship ship = new Destroyer();

            while (!ship.Sunken)
            {
                ship.Hit();
            }

            Field field = new VicinityField(ship);

            Assert.Equal(Occupant.Empty, field.GetOpponentView());
        }

        [Fact]
        public void FiredAtTwice()
        {
            Ship ship = new Destroyer();
            Field field = new VicinityField(ship);

            field.Hit();

            Assert.Throws<RuleViolationException>(field.Hit);
        }

        [Fact]
        public void FiredAtSunkenShip()
        {
            Ship ship = new Destroyer();

            while (!ship.Sunken)
            {
                ship.Hit();
            }

            Field field = new VicinityField(ship);

            Assert.Throws<RuleViolationException>(field.Hit);
        }
    }
}
