using System;
using Com.Lepecki.BattleshipGame.Engine.Data;

namespace Com.Lepecki.BattleshipGame.Engine.Model
{
    public abstract class Player : IObserver<GameEvent>
    {
        public Guid Id { get; }

        public abstract void OnNext(GameEvent value);

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }
    }
}
