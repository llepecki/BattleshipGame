using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Com.Lepecki.BattleshipGame.Engine.Data
{
    public interface IEventStore
    {
        Task<IEnumerable<GameEvent>> Get(Guid gameId);
    }
}
