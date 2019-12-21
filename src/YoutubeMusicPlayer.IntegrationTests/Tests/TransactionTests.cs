using System;
using System.IO;
using System.Threading.Tasks;
using System.Transactions;
using ChinhDo.Transactions.FileManager;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;
using YoutubeMusicPlayer.Framework;
using YoutubeMusicPlayer.MusicDownloading.Application.Services;
using YoutubeMusicPlayer.MusicDownloading.Domain;
using YoutubeMusicPlayer.MusicDownloading.Infrastructure;

namespace YoutubeMusicPlayer.MusicDownloading.IntegrationTests.Tests
{
    public class TransactionTests
    {
        [Fact]
        public async Task Test()
        {
            var manager = new FileManager();
            try
            {
                var dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "TestDb.db3");
                var musicPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments),
                    "music");

                DbMigrator.SetupDb($"Data Source={dbPath}");
                Directory.CreateDirectory(musicPath);
                await File.WriteAllBytesAsync($"{Path.Combine(musicPath, "test")}", new byte[] {1, 2, 3, 4, 5, 6});
                
                using var sqlConnection = new SqliteConnection($"Data Source={dbPath}");
                await sqlConnection.OpenAsync();
                var options = new DbContextOptionsBuilder<MusicDownloadingDbContext>()
                    .UseSqlite(sqlConnection)
                    
                    //.ConfigureWarnings(x => x.Ignore(RelationalEventId.AmbientTransactionEnlisted))
                    .Options;

                var dbTransaction = await sqlConnection.BeginTransactionAsync();

                using var dbContext = new MusicDownloadingDbContext(options);
                var repo = new SongRepository(dbContext);


                //await manager.CreateAsync($"{Path.Combine(musicPath, "test")}", new byte[] { 1, 2, 3, 4, 5, 6 });

                var current2 = Transaction.Current;
                //await repo.AddAsync(new Song(Guid.NewGuid(), Path.Combine(musicPath, "test"), "1", "test", "test")).ConfigureAwait(false);

                //await dbContext.SaveChangesAsync().ConfigureAwait(false);

                //await manager.CreateAsync($"{Path.Combine(musicPath, "test2")}", new byte[] { 6, 5, 4, 3, 2, 1 });

                await repo.AddAsync(new Song(Guid.NewGuid(), Path.Combine(musicPath, "test"), "2", "test2", "test2")).ConfigureAwait(false);

                await dbContext.SaveChangesAsync().ConfigureAwait(false);

                await manager.DeleteAsync($"{Path.Combine(musicPath, "test")}");

                throw new Exception("error");

                await dbTransaction.CommitAsync();
            }
            catch (Exception e)
            {
                await manager.Rollback();
                //means transaction has been already committed
            }
            
        }
    }
}