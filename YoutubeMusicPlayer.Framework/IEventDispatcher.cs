using System.Threading.Tasks;

namespace YoutubeMusicPlayer.Framework
{
    public interface IEventDispatcher
    {
        Task DispatchAsync<T> (params T[] @event) where T: IEvent;
    }
}