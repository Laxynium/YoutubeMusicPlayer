using System;
using System.Data;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SimpleInjector;
using YoutubeMusicPlayer.Framework.Decorators;
using YoutubeMusicPlayer.Framework.Messaging;

namespace YoutubeMusicPlayer.Framework
{
    public static class FrameworkStartup
    {
        public static void Initialize(Container container, string connectionString)
        {
            container.Register<ICommandDispatcher, CommandDispatcher>(Lifestyle.Singleton);

            container.Register<IEventDispatcher, EventDispatcher>(Lifestyle.Singleton);

            container.Register<IQueryDispatcher, QueryDispatcher>(Lifestyle.Singleton);

            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(TransactionScopeCommandHandlerDecorator<>), Lifestyle.Singleton);

            container.Register<FileManager>(Lifestyle.Scoped);

            container.Register(
                () => new SqliteConnection(connectionString), Lifestyle.Singleton);
            container.Register<DbTransaction>(
                () =>
                {
                    var connection = container.GetInstance<SqliteConnection>();
                    connection.Open();
                    return connection.BeginTransaction(IsolationLevel.ReadUncommitted);
                }, Lifestyle.Scoped);
            container.Register(() => new DbContextOptionsBuilder().UseSqlite(connectionString).Options, Lifestyle.Singleton);

            container.RegisterInstance<Func<UnitOfWork>>(()=>
                new UnitOfWork(
                    container.GetInstance<SqliteConnection>(),
                    container.GetInstance<DbTransaction>(),
                    container.GetInstance<FileManager>()));
        }
    }
}