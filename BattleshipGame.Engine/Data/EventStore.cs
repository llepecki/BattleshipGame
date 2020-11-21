using System;
using System.Collections.Generic;

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
    }
}
