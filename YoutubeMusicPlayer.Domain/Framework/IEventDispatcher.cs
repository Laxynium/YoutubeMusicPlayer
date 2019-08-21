using System.Threading.Tasks;

namespace YoutubeMusicPlayer.Domain.Framework
{
    public interface IEventDispatcher
    {
        Task DispatchAsync<T> (params T[] @event) where T: IEvent;
    }
}