using System;
using System.Threading.Tasks;
using System.Transactions;

namespace YoutubeMusicPlayer.Framework
{
    public class UnitOfWork : IDisposable
    {
        private readonly TransactionScope _transactionScope;

        public UnitOfWork()
        {
            _transactionScope = new TransactionScope(TransactionScopeOption.Suppress,TransactionScopeAsyncFlowOption.Enabled);
        }
        public Task Commit()
        {
            _transactionScope.Complete();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _transactionScope?.Dispose();
        }
    }
}