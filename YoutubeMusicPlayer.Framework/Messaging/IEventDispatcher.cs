using System.Threading.Tasks;

namespace YoutubeMusicPlayer.Framework.Messaging
{
    public interface IEventDispatcher
    {
        Task DispatchAsync<T> (params T[] @event) where T: IEvent;
    }
}