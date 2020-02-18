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
        private readonly Func<UnitOfWork> _unitOfWorkFactory;
        private readonly Container _container;

        public TransactionScopeCommandHandlerDecorator(Container container, 
            Func<ICommandHandler<T>> innerHandlerFactory, 
            Func<UnitOfWork> unitOfWorkFactory)
        {
            _innerHandlerFactory = innerHandlerFactory;
            _unitOfWorkFactory = unitOfWorkFactory;
            _container = container;
        }
        public async Task HandleAsync(T command) => await (IsOutermost()
            ? HandleOutermost(command)
            : HandleNested(command));

        private bool IsOutermost() 
            => AsyncScopedLifestyle.Scoped.GetCurrentScope(_container) is null;

        private async Task HandleOutermost(T command)
        {
            using var unused = AsyncScopedLifestyle.BeginScope(_container);

            var unitOfWork = _unitOfWorkFactory.Invoke();

            try
            {
                var handler = _innerHandlerFactory?.Invoke();
                await handler.HandleAsync(command);

                await unitOfWork.Commit();
            }
            catch (Exception e)
            {
                await unitOfWork.Rollback();
                throw;
            }
        }
        private async Task HandleNested(T command) 
            => await HandleCore(command);

        private async Task HandleCore(T command)
        {
            var handler = _innerHandlerFactory?.Invoke();
            await handler.HandleAsync(command);
        }
    }
}