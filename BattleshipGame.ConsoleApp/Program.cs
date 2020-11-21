using Com.Lepecki.BattleshipGame.Engine.Data;
using Com.Lepecki.BattleshipGame.Engine.Gears;
using Com.Lepecki.BattleshipGame.Engine.Model;

namespace Com.Lepecki.BattleshipGame.ConsoleApp
{
    internal class Program
    {
        private static void Main()
        {
            ICoordinateCalculator coordinateCalculator = new CoordinateCalculator();
            GridBuilderOptions gridBuilderOptions = new GridBuilderOptions();
            Game game = new Game(coordinateCalculator, gridBuilderOptions);
            EventStore store = new EventStore();
            ConsoleEventWriter writer = new ConsoleEventWriter();
            game.Subscribe(store);
            game.Subscribe(writer);
            Bootstrapper bootstrapper = new Bootstrapper(game, gridBuilderOptions.Size);

            for (int i = 0; i < gridBuilderOptions.Battleships; i++)
            {
                bootstrapper.RandomlyPlaceBattleship();
            }

            for (int i = 0; i < gridBuilderOptions.Destroyers; i++)
            {
                bootstrapper.RandomlyPlaceDestroyer();
            }

            ConsolePlayer player = new ConsolePlayer(game);
            player.Play();
        }
    }
}
