using Com.Lepecki.BattleshipGame.Engine.Data;
using Com.Lepecki.BattleshipGame.Engine.Gears;
using Com.Lepecki.BattleshipGame.Engine.Model;
using Xunit;

namespace Com.Lepecki.BattleshipGame.Engine.Tests.Model
{
    public class GameTests
    {
        [Fact]
        public void GameShouldFinish()
        {
            Game game = CreateGame();

            Assert.True(game.TryPut(new PlaceBattleshipEvent(game.Id, new Coordinate(1, 1), Orientation.South), out string _));
            Assert.True(game.TryPut(new PlaceDestroyerEvent(game.Id, new Coordinate(1, 3), Orientation.South), out string _));

            Assert.True(game.TryPut(new FireEvent(game.Id, new Coordinate(1, 1)), out string _));
            Assert.False(game.Finished);

            Assert.True(game.TryPut(new FireEvent(game.Id, new Coordinate(2, 1)), out string _));
            Assert.False(game.Finished);

            Assert.True(game.TryPut(new FireEvent(game.Id, new Coordinate(3, 1)), out string _));
            Assert.False(game.Finished);

            Assert.True(game.TryPut(new FireEvent(game.Id, new Coordinate(4, 1)), out string _));
            Assert.False(game.Finished);

            Assert.True(game.TryPut(new FireEvent(game.Id, new Coordinate(5, 1)), out string _));
            Assert.False(game.Finished);

            Assert.True(game.TryPut(new FireEvent(game.Id, new Coordinate(1, 3)), out string _));
            Assert.False(game.Finished);

            Assert.True(game.TryPut(new FireEvent(game.Id, new Coordinate(2, 3)), out string _));
            Assert.False(game.Finished);

            Assert.True(game.TryPut(new FireEvent(game.Id, new Coordinate(3, 3)), out string _));
            Assert.False(game.Finished);

            Assert.True(game.TryPut(new FireEvent(game.Id, new Coordinate(4, 3)), out string _));
            Assert.True(game.Finished);
        }

        private static Game CreateGame()
        {
            return new Game(new CoordinateCalculator(), new GridBuilderOptions { Battleships = 1, Destroyers = 1 });
        }
    }
}
