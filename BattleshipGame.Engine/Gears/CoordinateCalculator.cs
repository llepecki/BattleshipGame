using System.Collections.Generic;
using System.Linq;
using Com.Lepecki.BattleshipGame.Engine.Model;

namespace Com.Lepecki.BattleshipGame.Engine.Gears
{
    public class CoordinateCalculator : ICoordinateCalculator
    {
        public IReadOnlyCollection<Coordinate> CalculateShipCoordinates(int length, Coordinate stern, Orientation orientation)
        {
            bool horizontal = orientation == Orientation.East || orientation == Orientation.West;
            int direction = orientation == Orientation.East || orientation == Orientation.South ? 1 : -1;

            Coordinate[] coordinates = new Coordinate[length];

            for (int i = 0; i < length; i++)
            {
                coordinates[i] = horizontal ?
                    new Coordinate(stern.Row, stern.Column + direction * i) :
                    new Coordinate(stern.Row + direction * i, stern.Column);
            }

            return coordinates;
        }

        public IReadOnlyCollection<Coordinate> CalculateVicinityCoordinates(Coordinate coordinate)
        {
            return InnerCalculateVicinityCoordinates().ToArray();

            IEnumerable<Coordinate> InnerCalculateVicinityCoordinates()
            {
                yield return new Coordinate(coordinate.Row, coordinate.Column - 1);
                yield return new Coordinate(coordinate.Row, coordinate.Column + 1);
                yield return new Coordinate(coordinate.Row - 1, coordinate.Column);
                yield return new Coordinate(coordinate.Row + 1, coordinate.Column);
            }
        }
    }
}
