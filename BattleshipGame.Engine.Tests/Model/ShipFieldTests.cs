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
        public void FiredAtNotSunkenOwnerView()
        {
            Ship ship = new Battleship();
            Field field = new ShipField(ship);

            field.Hit();

            Assert.Equal(Occupant.Battleship, field.GetOwnerView());
        }

        [Fact]
        public void FiredAtNotSunkenOpponentView()
        {
            Ship ship = new Battleship();
            Field field = new ShipField(ship);

            field.Hit();

            Assert.Equal(Occupant.Hit, field.GetOpponentView());
        }

        [Fact]
        public void FiredAtSunkenOwnerView()
        {
            Ship ship = new Battleship();

            while (ship.Hits < ship.Length - 1)
            {
                ship.Hit();
            }

            Field field = new ShipField(ship);

            field.Hit();

            Assert.Equal(Occupant.Battleship, field.GetOwnerView());
        }

        [Fact]
        public void FiredAtSunkenOpponentView()
        {
            Ship ship = new Battleship();

            while (ship.Hits < ship.Length - 1)
            {
                ship.Hit();
            }

            Field field = new ShipField(ship);

            field.Hit();

            Assert.Equal(Occupant.Battleship, field.GetOpponentView());
        }

        [Fact]
        public void FiredAtTwice()
        {
            Ship ship = new Battleship();
            Field field = new ShipField(ship);
            field.Hit();
            Assert.Throws<RuleViolationException>(field.Hit);
        }
    }
}
