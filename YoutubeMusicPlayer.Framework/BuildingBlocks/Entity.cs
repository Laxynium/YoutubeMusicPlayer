using System.Collections.Generic;
using System.Linq;
using YoutubeMusicPlayer.Framework.Messaging;

namespace YoutubeMusicPlayer.Framework.BuildingBlocks
{
    public abstract class Entity<TId> where TId : ValueObject<TId>
    {
        public TId Id { get; protected set; }

        private readonly IList<IEvent> _events= new List<IEvent>();
        public IReadOnlyList<IEvent> Events => _events.ToList();

        protected void Apply(IEvent @event)
            => _events.Add(@event);

        protected Entity(TId id)
        {
            Id = id;
        }
    }
}