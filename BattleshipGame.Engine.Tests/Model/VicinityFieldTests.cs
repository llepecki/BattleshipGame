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
        public void FiredAtNotSunkedOwnerView()
        {
            Ship ship = new Destroyer();
            Field field = new VicinityField(ship);

            field.Fire();

            Assert.Equal(Occupant.Empty, field.GetOwnerView());
        }

        [Fact]
        public void FiredAtNotSunkedOpponentView()
        {
            Ship ship = new Destroyer();
            Field field = new VicinityField(ship);

            field.Fire();

            Assert.Equal(Occupant.Missed, field.GetOpponentView());
        }

        [Fact]
        public void FiredAtSunkedOwnerView()
        {
            Ship ship = new Destroyer();

            while (!ship.Sunked)
            {
                ship.Hit();
            }

            Field field = new VicinityField(ship);

            Assert.Equal(Occupant.Empty, field.GetOwnerView());
        }

        [Fact]
        public void FiredAtSunkedOpponentView()
        {
            Ship ship = new Destroyer();

            while (!ship.Sunked)
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

            field.Fire();

            Assert.Throws<RuleViolationException>(field.Fire);
        }

        [Fact]
        public void FiredAtSunkedShip()
        {
            Ship ship = new Destroyer();

            while (!ship.Sunked)
            {
                ship.Hit();
            }

            Field field = new VicinityField(ship);

            Assert.Throws<RuleViolationException>(field.Fire);
        }
    }
}
