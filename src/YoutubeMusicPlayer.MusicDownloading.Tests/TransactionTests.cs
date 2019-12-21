using System;
using System.Threading.Tasks;
using System.Transactions;
using ChinhDo.Transactions.FileManager;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Xunit;
using YoutubeMusicPlayer.MusicDownloading.Application.Repositories;
using YoutubeMusicPlayer.MusicDownloading.Domain;
using YoutubeMusicPlayer.MusicDownloading.Infrastructure;

namespace YoutubeMusicPlayer.MusicDownloading.Tests
{
    public class TransactionTests
    {
        [Fact]
        public async Task Test()
        {
            DbMigrator.SetupDb("Data Source=TestDb2.db");
            IFileManager fileManager = new TxFileManager();

            using var transaction = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled);
            using var sqlConnection = new SqliteConnection("Data Source=TestDb2.db");
            await sqlConnection.OpenAsync();
            var options = new DbContextOptionsBuilder<MusicDownloadingDbContext>()
                .UseSqlite(sqlConnection)
                .ConfigureWarnings(x => x.Ignore(RelationalEventId.AmbientTransactionWarning))
                .Options;

            var dbTransaction = await sqlConnection.BeginTransactionAsync();

            using var dbContext = new MusicDownloadingDbContext(options);
            var repo = new SongRepository(dbContext);

            if (!fileManager.DirectoryExists("music"))
                fileManager.CreateDirectory("music");

            fileManager.WriteAllBytes("music/test", new byte[] { 1, 2, 3, 4, 5, 6 });

            await repo.AddAsync(new Song(Guid.NewGuid(), "test", "1", "test", "test")).ConfigureAwait(false);

            await dbContext.SaveChangesAsync().ConfigureAwait(false);

            fileManager.WriteAllBytes("music/test2", new byte[] { 6, 5, 4, 3, 2, 1 });

            await repo.AddAsync(new Song(Guid.NewGuid(), "test2", "2", "test2", "test2")).ConfigureAwait(false);

            await dbContext.SaveChangesAsync().ConfigureAwait(false);

            //throw new Exception("error");

            await dbTransaction.CommitAsync();

            transaction.Complete();
        }
    }
}