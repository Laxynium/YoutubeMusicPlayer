using System.Linq;
using System.Threading.Tasks;
using Ninject;
using YoutubeMusicPlayer.Domain.Framework;

namespace YoutubeMusicPlayer.Droid.Framework
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IKernel _kernel;

        public CommandDispatcher(IKernel kernel)
        {
            _kernel = kernel;
        }
        public async Task DispatchAsync<T>(T command) where T : ICommand
        {
            var type = typeof(ICommandHandler<>);
            var genericType = type.MakeGenericType(command.GetType());
            dynamic handler = _kernel.GetAll(genericType).FirstOrDefault(x => genericType.IsInstanceOfType(x));
            await handler.HandleAsync(command);
        }
    }
}