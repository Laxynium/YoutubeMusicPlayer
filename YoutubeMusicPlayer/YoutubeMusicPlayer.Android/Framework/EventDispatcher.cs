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
        public async Task DispatchAsync<T>(T @event) where T : IEvent
        {
            foreach (var eventHandler in _kernel.GetAll<IEventHandler<T>>())
            {
                await eventHandler.HandleAsync(@event);
            }
        }
    }
}