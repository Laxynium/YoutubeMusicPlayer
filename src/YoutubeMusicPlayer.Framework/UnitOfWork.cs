using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace YoutubeMusicPlayer.Framework
{
    public class UnitOfWork
    {
        private readonly SqliteConnection _connection;
        private readonly DbTransaction _transaction;
        private readonly FileManager _fileManager;

        public UnitOfWork(SqliteConnection connection, DbTransaction transaction, FileManager fileManager)
        {
            _connection = connection;
            _transaction = transaction;
            _fileManager = fileManager;
        }

        public async Task Commit()
        {
            await _transaction.CommitAsync();
            //await _connection.CloseAsync();
        }

        public async Task Rollback()
        {
            //transaction is going to be rolled back automatically
            await _fileManager.Rollback();
        }
    }
}