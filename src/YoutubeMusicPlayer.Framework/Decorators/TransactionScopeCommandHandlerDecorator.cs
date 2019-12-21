using System;
using System.Threading.Tasks;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using YoutubeMusicPlayer.Framework.Messaging;

namespace YoutubeMusicPlayer.Framework.Decorators
{
    public class TransactionScopeCommandHandlerDecorator<T> : ICommandHandler<T> where T:ICommand
    {
        private readonly Func<ICommandHandler<T>> _innerHandlerFactory;
        private readonly Container _container;

        public TransactionScopeCommandHandlerDecorator(Container container, Func<ICommandHandler<T>> innerHandlerFactory)
        {
            _innerHandlerFactory = innerHandlerFactory;
            _container = container;
        }
        public async Task HandleAsync(T command)
        {
            using var _ = AsyncScopedLifestyle.BeginScope(_container);
            var handler = _innerHandlerFactory?.Invoke();
            await handler.HandleAsync(command);
        }
    }
}