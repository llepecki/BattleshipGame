using System.Collections.Generic;
using Com.Lepecki.BattleshipGame.Engine.Model;

namespace Com.Lepecki.BattleshipGame.Engine.Gears
{
    public interface ICoordinateCalculator
    {
        IReadOnlyCollection<Coordinate> CalculateShipCoordinates(int length, Coordinate stern, Orientation orientation);

        IReadOnlyCollection<Coordinate> CalculateVicinityCoordinates(Coordinate coordinate);
    }
}
