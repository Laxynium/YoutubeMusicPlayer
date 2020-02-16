using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleInjector;

namespace YoutubeMusicPlayer.Framework.Messaging
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
                var handlers = TryGetAllInstances(genericType);
                foreach (dynamic handler in handlers)
                {
                    await handler.HandleAsync((dynamic)@event);
                }
            }
        }

        //for some reason simple injector throws exception when there are no handlers
        private IEnumerable<object> TryGetAllInstances(Type genericType)
        {
            try
            {
                return _container.GetAllInstances(genericType);
            }
            catch (ActivationException e)
            {
                //TODO log missing handlers
                return Enumerable.Empty<object>();
            }
        }
    }
}