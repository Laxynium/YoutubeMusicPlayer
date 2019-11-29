using System.Threading.Tasks;
using SimpleInjector;

namespace YoutubeMusicPlayer.Framework
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly Container _container;

        public EventDispatcher(Container container)
        {
            _container = container;
        }
        public async Task DispatchAsync<T>(params T[] events) where T : IEvent
        {
            foreach (var @event in events)
            {
                var type = typeof(IEventHandler<>);
                var genericType = type.MakeGenericType(@event.GetType());
                var handlers = _container.GetAllInstances(genericType);
                foreach (dynamic handler in handlers)
                {
                    await handler.HandleAsync((dynamic)@event);
                }
            }
        }
    }
}