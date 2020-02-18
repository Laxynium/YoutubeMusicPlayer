using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using YoutubeMusicPlayer.MusicManagement.Domain.Entities;
using YoutubeMusicPlayer.MusicManagement.Domain.ValueObjects;

namespace YoutubeMusicPlayer.MusicManagement.Infrastructure.EF
{
    public class MusicManagementDbContext : DbContext
    {
        public DbSet<MainPlaylist> MainPlaylist { get; set; }
        //public DbSet<RegularPlaylist> RegularPlaylists { get; set; }
        public MusicManagementDbContext(DbContextOptions options, DbTransaction transaction = null):base(options)
        {
            if(transaction is { })
                Database.UseTransaction(transaction);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MainPlaylist>(
                m =>
                {
                    m.HasKey(x => x.Id);
                    m.Property(x=>x.Id).HasConversion(
                        x => x.Value,
                        x => new PlaylistId(x)
                        );
                    m.Property(x=>x.Name)
                        .HasConversion(
                            x => x.Value,
                            x => new PlaylistName(x)
                        );

                    //m.Property(x => x.Songs).HasField("_songs");
                    m.OwnsMany(
                        x => x.Songs,
                        s =>
                        {
                            s.Property(y => y.Id).HasConversion(
                                c => c.Value,
                                c => new SongId(c)
                            );
                            s.Property(y => y.SongPath).HasConversion(
                                c => c.Value,
                                c => new SongPath(c)
                            );
                            s.ToTable("music_management$main_playlist_song");
                        }
                    );

                    m.HasData(new MainPlaylist(new PlaylistId(Guid.NewGuid()), new PlaylistName("Main Playlist")));

                    m.ToTable("music_management$main_playlist");
                }
            );
            //modelBuilder.Entity<RegularPlaylist>().HasKey(x => x.Id);
            //modelBuilder.Entity<RegularPlaylist>().Property(x => x.Id).HasConversion(
            //    x => x.Value,
            //    x => new PlaylistId(x));
            //modelBuilder.Entity<RegularPlaylist>().Property(x => x.Name)
            //    .HasConversion(
            //        x => x.Value,
            //        x => new PlaylistName(x)
            //    );
            //modelBuilder.Entity<MainPlaylist>().OwnsMany(x => x.Songs).ToTable("MainPlaylistSongs");
            //modelBuilder.Entity<MainPlaylist>().ToTable("MainPlaylists");
        }
    }
}