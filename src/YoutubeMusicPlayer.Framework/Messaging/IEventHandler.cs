using System.Threading.Tasks;

namespace YoutubeMusicPlayer.Framework.Messaging
{
    public interface IEventHandler <in T> where T : IEvent
    {
        Task HandleAsync(T @event);
    }
}