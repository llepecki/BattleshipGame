using CommandLine;

namespace Com.Lepecki.BattleshipGame.ConsoleApp
{
    public class Options
    {
        [Option('v', "verbose", Required = false, Default = false, HelpText = "Print all game events to the console")]
        public bool Verbose { get; set; }

        [Option('p', "path", Required = false, Default = null, HelpText = "Path to save game history")]
        public string? Path { get; set; }
    }
}
