namespace YoutubeMusicPlayer.Domain.Framework
{
    public abstract class Entity<TId> where TId : ValueObject<TId>
    {
        public TId Id { get; protected set; }

        protected Entity(TId id)
        {
            Id = id;
        }
    }
}