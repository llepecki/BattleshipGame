using System;
using Com.Lepecki.BattleshipGame.Engine.Gears;
using Com.Lepecki.BattleshipGame.Engine.Model;
using Xunit;

namespace Com.Lepecki.BattleshipGame.Engine.Tests.Gears
{
    public class GridBuilderTests
    {
        private static readonly GridBuilderOptions Options = new GridBuilderOptions
        {
            Size = 10,
            Battleships = 2,
            Destroyers = 3
        };

        [Fact]
        public void ShouldThrowWhenTryingToBuildGridThatLacksShips()
        {
            GridBuilder builder = new GridBuilder(new CoordinateCalculator(), Options);

            builder.PlaceBattleship(new Coordinate(1, 1), Orientation.South);
            builder.PlaceDestroyer(new Coordinate(1, 3), Orientation.South);

            Assert.Throws<InvalidOperationException>(() => builder.Build());
        }
    }
}
