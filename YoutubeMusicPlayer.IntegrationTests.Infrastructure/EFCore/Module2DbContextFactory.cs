using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace YoutubeMusicPlayer.IntegrationTests.Infrastructure.EFCore
{
    public class Module2DbContextFactory : IDesignTimeDbContextFactory<Module2DbContext>
    {
        public Module2DbContext CreateDbContext(string[] args)
        {
            var connectionString1 = new SqliteConnectionStringBuilder() { DataSource = "module1.db3", Mode = SqliteOpenMode.ReadWriteCreate }.ToString();

            var builder1 = new DbContextOptionsBuilder();
            builder1.UseSqlite(connectionString1, x => x.MigrationsAssembly(typeof(Module2DbContext).Assembly.FullName).MigrationsHistoryTable("module2_migrations"));
            builder1.EnableDetailedErrors().EnableSensitiveDataLogging();
            return new Module2DbContext(builder1.Options);
        }
    }
}