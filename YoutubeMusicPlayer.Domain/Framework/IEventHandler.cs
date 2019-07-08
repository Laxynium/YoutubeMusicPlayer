using System.Threading.Tasks;

namespace YoutubeMusicPlayer.Domain.Framework
{
    public interface IEventHandler <in T> where T : IEvent
    {
        Task HandleAsync(T @event);
    }
}