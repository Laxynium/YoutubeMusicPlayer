using System.Threading.Tasks;

namespace YoutubeMusicPlayer.Framework
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<T>(T command) where T : ICommand;
    }
}