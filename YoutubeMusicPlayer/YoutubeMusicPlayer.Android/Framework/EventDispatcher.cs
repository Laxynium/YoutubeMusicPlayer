using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ninject;
using System.Threading.Tasks;
using YoutubeMusicPlayer.Domain.Framework;

namespace YoutubeMusicPlayer.Droid.Framework
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IKernel _kernel;

        public EventDispatcher(IKernel kernel)
        {
            _kernel = kernel;
        }
        public async Task DispatchAsync<T>(params T[] events) where T : IEvent
        {
            foreach (var @event in events)
            {
                var eventHandlerType = typeof(IEventHandler<>).MakeGenericType(@event.GetType());
                IEnumerable<object> handlers = _kernel.GetAll(eventHandlerType).Where(t => eventHandlerType.IsInstanceOfType(t)).ToList();
                foreach (var handler in handlers)
                {
                    var method = GetHandleAsyncMethod(handler, @event) ?? throw new InvalidOperationException("Method HandleAsync was not found.");
                    await (Task)method.Invoke(handler, new object[] { @event });
                }
            }
}

        private static MethodInfo GetHandleAsyncMethod<T>(object handler, T @event) where T : IEvent
        {
            return handler.GetType().GetMethods().FirstOrDefault(
                m => m.Name == "HandleAsync"
                     && m.GetParameters().Length == 1
                     && m.GetParameters().Any(x => x.ParameterType.IsInstanceOfType(@event))
            );
        }
    }
}