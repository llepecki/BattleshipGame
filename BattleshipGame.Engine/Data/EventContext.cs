using Microsoft.EntityFrameworkCore;

namespace Com.Lepecki.BattleshipGame.Engine.Data
{
    public class EventContext : DbContext
    {
#pragma warning disable CS8618
        public virtual DbSet<GameEvent> GameEvents { get; set; }
#pragma warning restore CS8618
    }
}
