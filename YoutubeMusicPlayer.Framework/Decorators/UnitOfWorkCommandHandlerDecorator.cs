using System.Threading.Tasks;
using System.Transactions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using YoutubeMusicPlayer.Framework.Messaging;

namespace YoutubeMusicPlayer.Framework.Decorators
{
    public class UnitOfWorkCommandHandlerDecorator<T> : ICommandHandler<T> where T:ICommand
    {
        private readonly SqliteConnection _connection;
        private readonly DbContext _dbContext;
        private readonly ICommandHandler<T> _innerHandler;

        public UnitOfWorkCommandHandlerDecorator(SqliteConnection connection, DbContext dbContext, ICommandHandler<T> innerHandler)
        {
            _connection = connection;
            _dbContext = dbContext;
            _innerHandler = innerHandler;
        }
        public async Task HandleAsync(T command)
        {
            using var transaction = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled);
            await _connection.OpenAsync();
            var dbTransaction = await _connection.BeginTransactionAsync();

            await _innerHandler.HandleAsync(command);

            await _dbContext.SaveChangesAsync();
            await dbTransaction.CommitAsync();
            transaction.Complete();
        }
    }
}