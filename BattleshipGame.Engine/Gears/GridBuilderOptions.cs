namespace Com.Lepecki.BattleshipGame.Engine.Gears
{
    public record GridBuilderOptions
    {
        public int Cols { get; init; } = 10;
        public int Rows { get; init; } = 10;
        public int RequiredBattleshipCount { get; init; } = 1;
        public int RequiredDestroyerCount { get; init; } = 2;
    }
}
