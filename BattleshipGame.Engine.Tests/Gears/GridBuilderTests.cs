using System;
using Com.Lepecki.BattleshipGame.Engine.Gears;
using Xunit;

namespace Com.Lepecki.BattleshipGame.Engine.Tests.Gears
{
    public class GridBuilderTests
    {
        private static readonly GridBuilderOptions Options = new GridBuilderOptions
        {
            Cols = 10,
            Rows = 10,
            RequiredBattleshipCount = 2,
            RequiredDestroyerCount = 3
        };

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 2)]
        [InlineData(0, 3)]
        [InlineData(1, 3)]
        [InlineData(2, 0)]
        [InlineData(2, 1)]
        [InlineData(2, 2)]
        public void ShouldThrowWhenTryingToBuildGridThatLacksShips(int battleships, int destroyers)
        {
            GridBuilder builder = new GridBuilder(new CoordinateCalculator(), Options);

            for (int i = 0; i < battleships; i++)
            {
                builder.PlaceBattleship();
            }

            for (int i = 0; i < destroyers; i++)
            {
                builder.PlaceDestroyer();
            }

            Assert.Throws<InvalidOperationException>(() => builder.Build());
        }
    }
}
