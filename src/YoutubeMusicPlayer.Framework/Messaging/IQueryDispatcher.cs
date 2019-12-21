using System.Threading.Tasks;

namespace YoutubeMusicPlayer.Framework.Messaging
{
    public interface IQueryDispatcher
    {
        Task<TResult> DispatchAsync<TResult>(IQuery<TResult> query);
    }
}