using System.Collections.Generic;
using Com.Lepecki.BattleshipGame.Engine.Gears;
using Com.Lepecki.BattleshipGame.Engine.Model;
using Xunit;

namespace Com.Lepecki.BattleshipGame.Engine.Tests.Gears
{
    public class CoordinateCalculatorTests
    {
        [Theory]
        [MemberData(nameof(ShipTestCases))]
        public void ShouldCreateExcectedShipCoordinates(int length, Coordinate stern, Orientation orientation, Coordinate[] expectedCoordinates)
        {
            ICoordinateCalculator calculator = new CoordinateCalculator();
            Assert.Equal(expectedCoordinates, calculator.CalculateShipCoordinates(length, stern, orientation));
        }

        [Theory]
        [MemberData(nameof(VicinityTestCases))]
        public void ShouldCreateExcectedVicinityCoordinates(Coordinate coordinate, Coordinate[] expectedCoordinates)
        {
            ICoordinateCalculator calculator = new CoordinateCalculator();
            Assert.Equal(expectedCoordinates, calculator.CalculateVicinityCoordinates(coordinate));
        }

        public static IEnumerable<object[]> ShipTestCases()
        {
            yield return new object[]
            {
                5,
                new Coordinate(0, 0),
                Orientation.North,
                new Coordinate[]
                {
                    new Coordinate(0, 0),
                    new Coordinate(-1, 0),
                    new Coordinate(-2, 0),
                    new Coordinate(-3, 0),
                    new Coordinate(-4, 0)
                }
            };

            yield return new object[]
            {
                5,
                new Coordinate(1, 0),
                Orientation.South,
                new Coordinate[]
                {
                    new Coordinate(1, 0),
                    new Coordinate(2, 0),
                    new Coordinate(3, 0),
                    new Coordinate(4, 0),
                    new Coordinate(5, 0)
                }
            };

            yield return new object[]
            {
                5,
                new Coordinate(0, 1),
                Orientation.West,
                new Coordinate[]
                {
                    new Coordinate(0, 1),
                    new Coordinate(0, 0),
                    new Coordinate(0, -1),
                    new Coordinate(0, -2),
                    new Coordinate(0, -3)
                }
            };

            yield return new object[]
            {
                5,
                new Coordinate(5, 5),
                Orientation.East,
                new Coordinate[]
                {
                    new Coordinate(5, 5),
                    new Coordinate(5, 6),
                    new Coordinate(5, 7),
                    new Coordinate(5, 8),
                    new Coordinate(5, 9)
                }
            };
        }

        public static IEnumerable<object[]> VicinityTestCases()
        {
            yield return new object[]
            {
                new Coordinate(5, 5),
                new Coordinate[]
                {
                    new Coordinate(5, 4),
                    new Coordinate(5, 6),
                    new Coordinate(4, 5),
                    new Coordinate(6, 5)
                }
            };
        }
    }
}
