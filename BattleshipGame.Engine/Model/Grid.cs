using System;
using System.Collections.Generic;
using System.Linq;

namespace Com.Lepecki.BattleshipGame.Engine.Model
{
    public class Grid
    {
        private readonly IReadOnlyDictionary<Coordinate, Field> _fields;
        private readonly IReadOnlyCollection<Ship> _ships;

        public Grid(IReadOnlyDictionary<Coordinate, Field> fields, IReadOnlyCollection<Ship> ships)
        {
            _fields = fields;
            _ships = ships;
            MaxHits = _ships.Sum(ship => ship.Length);
        }

        public void Hit(Coordinate coordinate)
        {
            if (_fields.ContainsKey(coordinate))
            {
                _fields[coordinate].Fire();
            }

            throw new ArgumentOutOfRangeException(nameof(coordinate));
        }

        public int MaxHits { get; }

        public int CurrentHits => _ships.Sum(ship => ship.Hits);
    }
}
