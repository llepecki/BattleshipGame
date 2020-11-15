using Com.Lepecki.BattleshipGame.Engine.Exceptions;
using Com.Lepecki.BattleshipGame.Engine.Model;
using Xunit;

namespace Com.Lepecki.BattleshipGame.Engine.Tests.Model
{
    public class ShipFieldTests
    {
        [Fact]
        public void NotFiredAtOwnerView()
        {
            Ship ship = new Battleship();
            Field field = new ShipField(ship);

            Assert.Equal(Occupant.Battleship, field.GetOwnerView());
        }

        [Fact]
        public void NotFiredAtOpponentView()
        {
            Ship ship = new Battleship();
            Field field = new ShipField(ship);

            Assert.Equal(Occupant.Hidden, field.GetOpponentView());
        }

        [Fact]
        public void FiredAtNotSunkedOwnerView()
        {
            Ship ship = new Battleship();
            Field field = new ShipField(ship);

            field.Fire();

            Assert.Equal(Occupant.Battleship, field.GetOwnerView());
        }

        [Fact]
        public void FiredAtNotSunkedOpponentView()
        {
            Ship ship = new Battleship();
            Field field = new ShipField(ship);

            field.Fire();

            Assert.Equal(Occupant.Hit, field.GetOpponentView());
        }

        [Fact]
        public void FiredAtSunkedOwnerView()
        {
            Ship ship = new Battleship();

            while (ship.Hits < ship.Length - 1)
            {
                ship.Hit();
            }

            Field field = new ShipField(ship);

            field.Fire();

            Assert.Equal(Occupant.Battleship, field.GetOwnerView());
        }

        [Fact]
        public void FiredAtSunkedOpponentView()
        {
            Ship ship = new Battleship();

            while (ship.Hits < ship.Length - 1)
            {
                ship.Hit();
            }

            Field field = new ShipField(ship);

            field.Fire();

            Assert.Equal(Occupant.Battleship, field.GetOpponentView());
        }

        [Fact]
        public void FiredAtTwice()
        {
            Ship ship = new Battleship();
            Field field = new ShipField(ship);
            field.Fire();
            Assert.Throws<RuleViolationException>(field.Fire);
        }
    }
}
