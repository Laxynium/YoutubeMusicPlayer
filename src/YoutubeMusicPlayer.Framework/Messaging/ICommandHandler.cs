using System.Threading.Tasks;

namespace YoutubeMusicPlayer.Framework.Messaging
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}