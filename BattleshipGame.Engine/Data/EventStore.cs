using System;

namespace Com.Lepecki.BattleshipGame.Engine.Data
{
    public class EventStore : IObserver<GameEvent>
    {
        private readonly EventContext _eventContext;

        public EventStore(EventContext eventContext)
        {
            _eventContext = eventContext;
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(GameEvent gameEvent)
        {
            _eventContext.Add(gameEvent);
            _eventContext.SaveChanges();
        }
    }
}
