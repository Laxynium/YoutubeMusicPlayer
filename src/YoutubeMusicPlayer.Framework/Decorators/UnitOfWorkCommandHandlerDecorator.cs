using System;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using YoutubeMusicPlayer.Framework.Messaging;

namespace YoutubeMusicPlayer.Framework.Decorators
{
    public class UnitOfWorkCommandHandlerDecorator<T> : ICommandHandler<T> where T:ICommand
    {
        private readonly SqliteConnection _connection;
        private readonly DbContext _dbContext;
        private readonly FileManager _fileManager;
        private readonly ICommandHandler<T> _innerHandler;

        public UnitOfWorkCommandHandlerDecorator(SqliteConnection connection, DbContext dbContext, FileManager fileManager, ICommandHandler<T> innerHandler)
        {
            _connection = connection;
            _dbContext = dbContext;
            _fileManager = fileManager;
            _innerHandler = innerHandler;
        }
        public async Task HandleAsync(T command)
        {
            try
            {
                await _connection.OpenAsync();
                var dbTransaction = await _connection.BeginTransactionAsync();

                await _innerHandler.HandleAsync(command);

                await _dbContext.SaveChangesAsync();
                await dbTransaction.CommitAsync();
            }
            catch (Exception)
            {
                await _fileManager.Rollback();
                throw;
            }
            
        }
    }
}