using System;
using YoutubeMusicPlayer.Domain.Framework;
using YoutubeMusicPlayer.Domain.MusicDownloading.Events;
using YoutubeMusicPlayer.Domain.MusicPlayingNew;
using YoutubeMusicPlayer.Domain.SharedKernel;

namespace YoutubeMusicPlayer.Domain.MusicDownloading
{
    public class Song : Entity<SongId>
    {
        public string YoutubeId { get; }
        public string Title { get; }
        public string ImageSource { get; }
        public SongPath SongPath { get; }

        public Song(SongPath songPath, string youtubeId, string title, string imageSource) : this(SongId.FromGuid(Guid.NewGuid()), songPath, youtubeId, title, imageSource)
        {
        }
        public Song(SongId id, SongPath songPath, string youtubeId, string title, string imageSource) : base(id)
        {
            SongPath = songPath;
            YoutubeId = youtubeId;
            Title = title;
            ImageSource = imageSource;
            Apply(new SongCreated(Id,youtubeId,songPath,title,imageSource));
        }
    }
}