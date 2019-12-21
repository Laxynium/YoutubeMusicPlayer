using System.Threading.Tasks;
using SimpleInjector;

namespace YoutubeMusicPlayer.Framework.Messaging
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly Container _container;

        public QueryDispatcher(Container container)
        {
            _container = container;
        }

        public async Task<TResult> DispatchAsync<TResult>(IQuery<TResult> query)
        {
            var type = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));

            dynamic handler = _container.GetInstance(type);

            return (TResult)await handler.HandleAsync((dynamic)query);
        }
    }
}