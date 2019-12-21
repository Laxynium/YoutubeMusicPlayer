using System.Transactions;
using Microsoft.EntityFrameworkCore;
using YoutubeMusicPlayer.MusicDownloading.Domain;

namespace YoutubeMusicPlayer.MusicDownloading.Infrastructure
{
    public sealed class MusicDownloadingDbContext : DbContext
    {
        public DbSet<Song> Songs { get; set; }
        public MusicDownloadingDbContext(DbContextOptions options):base(options)
        {
            Database.AutoTransactionsEnabled = false;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Song>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Song>()
                .ToTable("Songs");
        }
    }
}