using System.Collections.Generic;
using System.Threading.Tasks;

namespace YoutubeMusicPlayer.Framework.Messaging
{
    public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}