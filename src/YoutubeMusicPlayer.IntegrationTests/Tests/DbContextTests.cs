using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Xunit;
using YoutubeMusicPlayer.MusicDownloading.Domain;
using YoutubeMusicPlayer.MusicDownloading.Infrastructure;

namespace YoutubeMusicPlayer.MusicDownloading.IntegrationTests.Tests
{
    public class DbContextTests
    {
        [Fact]
        public async Task Test()
        {

            try
            {
                var sqliteConnection = GetSqliteConnection();
                using (var context = CreateDbContext(sqliteConnection))
                {

                    await sqliteConnection.OpenAsync();
                    var dbTransaction = await sqliteConnection.BeginTransactionAsync();

                    var list = await context.Songs.ToListAsync();


                    await context.Songs.AddAsync(new Song(Guid.NewGuid(), "abc", "123", "abc", "abc"));

                    var list2 = await context.Songs.ToListAsync();

                    await context.SaveChangesAsync();

                    await dbTransaction.CommitAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private MusicDownloadingDbContext CreateDbContext(SqliteConnection connection)
        {
            var dbContext = new MusicDownloadingDbContext(new DbContextOptionsBuilder()
                .UseSqlite(connection)
                .ConfigureWarnings(x => x.Ignore(RelationalEventId.AmbientTransactionEnlisted))
                .Options);
            return dbContext;
        }

        private SqliteConnection GetSqliteConnection()
        {
            var dbFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments),
                "TestDbFile.db3");

            var sqliteConnection = new SqliteConnection($"Data Source={dbFile};");

            DbMigrator.SetupDb($"Data Source={dbFile}");
            
            return sqliteConnection;
        }
    }
}