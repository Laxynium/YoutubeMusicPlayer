using System.Threading.Tasks;

namespace YoutubeMusicPlayer.Domain.Framework
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<T>(T command) where T : ICommand;
    }
}