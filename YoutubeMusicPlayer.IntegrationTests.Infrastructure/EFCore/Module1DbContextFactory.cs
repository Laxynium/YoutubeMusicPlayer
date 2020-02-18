using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace YoutubeMusicPlayer.IntegrationTests.Infrastructure.EFCore
{
    public class Module1DbContextFactory : IDesignTimeDbContextFactory<Module1DbContext>
    {
        public Module1DbContext CreateDbContext(string[] args)
        {
            var connectionString1 = new SqliteConnectionStringBuilder() { DataSource = "module1.db3", Mode = SqliteOpenMode.ReadWriteCreate }.ToString();

            var builder1 = new DbContextOptionsBuilder();
            builder1.UseSqlite(connectionString1, x => x.MigrationsAssembly(typeof(Module1DbContext).Assembly.FullName).MigrationsHistoryTable("module1_migrations"));
            builder1.EnableDetailedErrors().EnableSensitiveDataLogging();
            return new Module1DbContext(builder1.Options);
        }
    }
}