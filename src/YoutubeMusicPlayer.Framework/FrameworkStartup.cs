using SimpleInjector;
using YoutubeMusicPlayer.Framework.Decorators;
using YoutubeMusicPlayer.Framework.Messaging;

namespace YoutubeMusicPlayer.Framework
{
    public static class FrameworkStartup
    {
        public static void Initialize(Container container)
        {
            container.Register<ICommandDispatcher, CommandDispatcher>(Lifestyle.Singleton);

            container.Register<IEventDispatcher, EventDispatcher>(Lifestyle.Singleton);

            container.Register<IQueryDispatcher, QueryDispatcher>(Lifestyle.Singleton);

            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>), Lifestyle.Scoped);

            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(TransactionScopeCommandHandlerDecorator<>), Lifestyle.Singleton);

            container.Register<FileManager>(Lifestyle.Scoped);
        }
    }
}