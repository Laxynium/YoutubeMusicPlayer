using System.Threading.Tasks;

namespace YoutubeMusicPlayer.Domain.Framework
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}