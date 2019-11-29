using System.Threading.Tasks;
using SimpleInjector;

namespace YoutubeMusicPlayer.Framework
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly Container _container;

        public CommandDispatcher(Container container)
        {
            _container = container;
        }
        public async Task DispatchAsync<T>(T command) where T : ICommand
        {
            var type = typeof(ICommandHandler<>).MakeGenericType(command.GetType());

            dynamic handler = _container.GetInstance(type);

            await handler.HandleAsync((dynamic)command);
        }
    }
}