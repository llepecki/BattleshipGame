using System;
using Com.Lepecki.BattleshipGame.Engine.Data;

namespace Com.Lepecki.BattleshipGame.ConsoleApp
{
    internal class ConsoleEventWriter : IObserver<GameEvent>
    {
        public void OnNext(GameEvent gameEvent)
        {
            Console.WriteLine($"# {gameEvent}");
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }
    }
}
