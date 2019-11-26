using System.Threading.Tasks;

namespace YoutubeMusicPlayer.Framework
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}