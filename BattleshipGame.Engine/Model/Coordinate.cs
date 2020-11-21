namespace Com.Lepecki.BattleshipGame.Engine.Model
{
    public readonly struct Coordinate
    {
        public Coordinate(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int Row { get; }

        public int Column { get; }

        public override string ToString()
        {
            return $"({Row}, {Column})";
        }
    }
}
