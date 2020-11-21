using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Com.Lepecki.BattleshipGame.Engine.Data;
using Com.Lepecki.BattleshipGame.Engine.Model;

namespace Com.Lepecki.BattleshipGame.ConsoleApp
{
    public class ConsolePlayer
    {
        private static readonly Regex inputPattern = new Regex("^[A-J]([1-9]|10)$");

        private readonly Game _game;

        public ConsolePlayer(Game game)
        {
            _game = game;
        }

        public void Play()
        {
            Console.WriteLine("# [q]uit game, [v]iew grid or fire, by entering a coordinate, e.g. a4");

            while (_game.Status != GameStatus.Finished)
            {
                Console.Write("> ");
                string input = Console.ReadLine();

                if (input.ToUpperInvariant() == "Q")
                {
                    Console.WriteLine("# Bye, bye");
                    return;
                }

                if (input.ToUpperInvariant() == "V")
                {
                    Occupant[,] view = _game.GetOpponentView();
                    Print(view);
                    continue;
                }

                if (TryGetCoordinate(input, out Coordinate coordinate))
                {
                    Console.WriteLine($"# Parsed {coordinate}");
                    GameEvent fireEvent = new FireEvent(_game.Id, coordinate);

                    if (!_game.TryPut(fireEvent, out string message))
                    {
                        Console.WriteLine($"# {message}");
                    }
                }
                else
                {
                    Console.WriteLine($"# Coordinate out of range");
                }
            }
        }

        private bool TryGetCoordinate(string input, out Coordinate coordinate)
        {
            string upperInput = input.ToUpperInvariant();

            if (inputPattern.IsMatch(upperInput))
            {
                coordinate = new Coordinate(upperInput[0] - 'A' + 1, int.Parse(upperInput.Substring(1, upperInput.Length - 1)));
                return true;
            }

            coordinate = new Coordinate();
            return false;
        }

        private static void Print(Occupant[,] view)
        {
            string head = string.Concat(Enumerable.Range(0, view.GetLength(0) + 1).Select(row => row == 0 ? " " : $"{row,3}"));
            Console.WriteLine(head);

            for (int i = 0; i < view.GetLength(0); i++)
            {
                StringBuilder builder = new StringBuilder(view.GetLength(1));
                builder.Append(((char)('A' + i)).ToString());

                for (int j = 0; j < view.GetLength(1); j++)
                {
                    builder.Append("  ");

                    switch (view[i, j])
                    {
                        case Occupant.Hidden:
                            builder.Append(".");
                            break;

                        case Occupant.Missed:
                            builder.Append("x");
                            break;

                        case Occupant.Hit:
                            builder.Append("!");
                            break;

                        case Occupant.Empty:
                            builder.Append("o");
                            break;

                        case Occupant.Battleship:
                            builder.Append("B");
                            break;

                        case Occupant.Destroyer:
                            builder.Append("D");
                            break;

                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                Console.WriteLine(builder.ToString());
            }
        }
    }
}
