using System;
using Com.Lepecki.BattleshipGame.Engine.Data;
using Com.Lepecki.BattleshipGame.Engine.Exceptions;
using Com.Lepecki.BattleshipGame.Engine.Model;

namespace Com.Lepecki.BattleshipGame.ConsoleApp
{
    public class Bootstrapper
    {
        private const int maxAttempts = 10000;

        private static readonly Random random = new Random();

        private readonly Game _game;
        private readonly int _size;

        public Bootstrapper(Game game, int size)
        {
            _game = game;
            _size = size;
        }

        public void RandomlyPlaceBattleship()
        {
            int attempts = 0;
            (Coordinate stern, Orientation orientation) placement = GetRandomPlacement();
            PlaceBattleshipEvent pbe = new PlaceBattleshipEvent(_game.Id, placement.stern, placement.orientation);

            while (!_game.TryPut(pbe, out string _) && attempts < maxAttempts)
            {
                placement = GetRandomPlacement();
                pbe = new PlaceBattleshipEvent(_game.Id, placement.stern, placement.orientation);
                attempts++;
            }

            if (attempts == maxAttempts)
            {
                throw new RandomPlacementException($"Failed to randomly place a {nameof(Battleship)}");
            }
        }

        public void RandomlyPlaceDestroyer()
        {
            int attempts = 0;
            (Coordinate stern, Orientation orientation) placement = GetRandomPlacement();
            PlaceDestroyerEvent pde = new PlaceDestroyerEvent(_game.Id, placement.stern, placement.orientation);

            while (!_game.TryPut(pde, out string _) && attempts < maxAttempts)
            {
                placement = GetRandomPlacement();
                pde = new PlaceDestroyerEvent(_game.Id, placement.stern, placement.orientation);
                attempts++;
            }

            if (attempts == maxAttempts)
            {
                throw new RandomPlacementException($"Failed to randomly place a {nameof(Destroyer)}");
            }
        }

        private (Coordinate coordinate, Orientation orientation) GetRandomPlacement()
        {
            Coordinate randomCoordinate = new Coordinate(random.Next(1, _size + 1), random.Next(1, _size + 1));

            int randomOrientationNumber = random.Next(0, 4);

            Orientation randomOrientation = randomOrientationNumber switch
            {
                0 => Orientation.North,
                1 => Orientation.South,
                2 => Orientation.West,
                3 => Orientation.East,
                _ => throw new ArgumentOutOfRangeException()
            };

            return (randomCoordinate, randomOrientation);
        }
    }
}
