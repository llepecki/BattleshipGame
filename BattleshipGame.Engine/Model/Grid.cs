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
            if (!_fields.ContainsKey(coordinate))
            {
                throw new ArgumentOutOfRangeException(nameof(coordinate));
            }

            _fields[coordinate].Hit();
        }

        public int MaxHits { get; }

        public int CurrentHits => _ships.Sum(ship => ship.Hits);

        public Occupant[,] GetOpponentView()
        {
            List<Occupant[]> result = new List<Occupant[]>();
            IEnumerable<IGrouping<int, Occupant>> groupping = _fields.GroupBy(by => by.Key.Row, selector => selector.Value.GetOpponentView());
            
            foreach (IGrouping<int, Occupant> group in groupping)
            {
                result.Add(group.ToArray());
            }

            Occupant[,] view = new Occupant[result.Count, result.Count];

            for (int i = 0; i < result.Count; i++)
            {
                for (int j = 0; j < result.Count; j++)
                {
                    view[i, j] = result[i][j];
                }
            }

            return view;
        }
    }
}
