using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using YoutubeMusicPlayer.MusicManagement.Infrastructure.EF;

namespace YoutubeMusicPlayer.MigrationRunner
{
    public class Program
    {
        public static void Main(string[] args)
        {

        }
    }
    internal sealed class MusicManagementDbContextFactory : IDesignTimeDbContextFactory<MusicManagementDbContext>
    {
        public MusicManagementDbContext CreateDbContext(string[] args)
        {
            return new MusicManagementDbContext(new DbContextOptionsBuilder().UseSqlite("not_empty").Options);
        }
    }
}