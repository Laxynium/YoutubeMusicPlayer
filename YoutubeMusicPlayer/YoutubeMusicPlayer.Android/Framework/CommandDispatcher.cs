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
            var handler = _kernel.Get<ICommandHandler<T>>();
            await handler.HandleAsync(command);
        }
    }
}