using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Com.Lepecki.BattleshipGame.Engine.Data
{
    public class EventStore : IObserver<GameEvent>
    {
        private readonly Dictionary<Guid, List<GameEvent>> _gameEvents = new Dictionary<Guid, List<GameEvent>>();

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(GameEvent gameEvent)
        {
            if (!_gameEvents.ContainsKey(gameEvent.GameId))
            {
                _gameEvents[gameEvent.GameId] = new List<GameEvent>();
            }

            _gameEvents[gameEvent.GameId].Add(gameEvent);
        }

        public void Save(string path)
        {
            foreach (Guid gameId in _gameEvents.Keys)
            {
                string fileName = gameId.ToString("N");

                File.WriteAllLines(
                    Path.Combine(path, $"{fileName}.txt"),
                    _gameEvents[gameId].Select(gameEvent => gameEvent.ToString()));
            }
        }
    }
}
