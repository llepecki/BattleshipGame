namespace Com.Lepecki.BattleshipGame.Engine.Gears
{
    public record GridBuilderOptions
    {
        public int Size { get; init; } = 10;
        public int Battleships { get; init; } = 1;
        public int Destroyers { get; init; } = 2;
    }
}
