using Microsoft.EntityFrameworkCore;
using YoutubeMusicPlayer.MusicManagement.Domain;

namespace YoutubeMusicPlayer.MusicManagement
{
    public class MusicManagementDbContext : DbContext
    {
        public DbSet<Playlist> Playlists { get; set; }
        public MusicManagementDbContext(DbContextOptions options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Playlist>().HasKey(x => x.Id);
            modelBuilder.Entity<Playlist>().Property(x => x.Id).HasConversion(x => x.Value, x => new PlaylistId(x));
        }
    }
}