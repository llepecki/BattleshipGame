using System;
using System.Collections.Generic;
using System.Linq;
using Com.Lepecki.BattleshipGame.Engine.Model;

namespace Com.Lepecki.BattleshipGame.Engine.Gears
{
    public class GridBuilder
    {
        private readonly ICoordinateCalculator _coordinateCalculator;
        private readonly GridBuilderOptions _options;
        private readonly Dictionary<Coordinate, Field> _fields;
        private readonly List<Ship> _ships = new List<Ship>();
        private readonly List<Coordinate> _lockedCoordinates = new List<Coordinate>();

        private int _currentBattleshipCount = 0;
        private int _currentDestroyerCount = 0;

        public GridBuilder(ICoordinateCalculator coordinateCalculator, GridBuilderOptions options)
        {
            _coordinateCalculator = coordinateCalculator;
            _options = options;
            _fields = new Dictionary<Coordinate, Field>(_options.Size * _options.Size);

            for (int c = 1; c < _options.Size + 1; c++)
            {
                for (int r = 1; r < _options.Size + 1; r++)
                {
                    _fields.Add(new Coordinate(r, c), new EmptyField());
                }
            };
        }

        public void PlaceBattleship(Coordinate stern, Orientation orientation)
        {
            if (_currentBattleshipCount == _options.Battleships)
            {
                throw new InvalidOperationException($"All {nameof(Battleship)}s are already in place");
            }

            if (!TryPlaceShip(new Battleship(), stern, orientation))
            {
                throw new InvalidOperationException($"Failed to place a {nameof(Battleship)} at ({stern.Column}, {stern.Row}) {orientation}");
            }

            _currentBattleshipCount++;
        }

        public void PlaceDestroyer(Coordinate stern, Orientation orientation)
        {
            if (_currentDestroyerCount == _options.Destroyers)
            {
                throw new InvalidOperationException($"All {nameof(Destroyer)}s are already in place");
            }

            if (!TryPlaceShip(new Destroyer(), stern, orientation))
            {
                throw new InvalidOperationException($"Failed to place a {nameof(Destroyer)} at ({stern.Column}, {stern.Row}) {orientation}");
            }

            _currentDestroyerCount++;
        }

        public bool CanBuild =>
            _currentBattleshipCount == _options.Battleships &&
            _currentDestroyerCount == _options.Destroyers;

        public Grid Build()
        {
            if (!CanBuild)
            {
                throw new InvalidOperationException($"Not enough ships to build the grid: {_currentBattleshipCount}/{_options.Battleships} {nameof(Battleship)}(s), {_currentDestroyerCount}/{_options.Destroyers} {nameof(Destroyer)}(s)");
            }

            return new Grid(_fields, _ships);
        }

        private bool TryPlaceShip(Ship ship, Coordinate stern, Orientation orientation)
        {
            IReadOnlyCollection<Coordinate> shipCoordinates = _coordinateCalculator.CalculateShipCoordinates(ship.Length, stern, orientation);

            if (shipCoordinates.All(IsWithinGrid) && !shipCoordinates.Any(IsUnavailable))
            {
                IEnumerable<Coordinate> vicinityCoordinatesWithinGrid = shipCoordinates
                    .Select(_coordinateCalculator.CalculateVicinityCoordinates)
                    .SelectMany(coordinates => coordinates)
                    .Except(shipCoordinates)
                    .Where(IsWithinGrid);

                foreach (Coordinate coordinate in shipCoordinates)
                {
                    _fields[coordinate] = new ShipField(ship);
                    _lockedCoordinates.Add(coordinate);
                }

                foreach (Coordinate coordinate in vicinityCoordinatesWithinGrid)
                {
                    _fields[coordinate] = new VicinityField(ship);
                    _lockedCoordinates.Add(coordinate);
                }

                _ships.Add(ship);
                return true;
            }

            return false;
        }

        private bool IsWithinGrid(Coordinate coordinate)
        {
            return _fields.ContainsKey(coordinate);
        }

        private bool IsUnavailable(Coordinate coordinate)
        {
            return _lockedCoordinates.Contains(coordinate);
        }
    }
}
