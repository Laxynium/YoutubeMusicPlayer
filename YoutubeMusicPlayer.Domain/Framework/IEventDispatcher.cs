using System.Threading.Tasks;

namespace YoutubeMusicPlayer.Domain.Framework
{
    public interface IEventDispatcher
    {
        Task DispatchAsync<T> (T @event) where T: IEvent;
    }
}