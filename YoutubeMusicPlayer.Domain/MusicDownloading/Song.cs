using System;
using SQLite;
using YoutubeMusicPlayer.Domain.SharedKernel;

namespace YoutubeMusicPlayer.Domain.MusicDownloading
{
    public class Song
    {
        public SongId Id { get; }
        public string YoutubeId { get; }
        public string Title { get; }
        public string ImageSource { get; }
        public string FilePath { get; }

        public Song(string youtubeId, string title, string imageSource, string filePath)
            :this(SongId.FromGuid(Guid.NewGuid()),youtubeId,title,imageSource,filePath)
        {
        }

        public Song(SongId id, string youtubeId, string title, string imageSource, string filePath)
        {
            Id = id;
            YoutubeId = youtubeId;
            Title = title;
            ImageSource = imageSource;
            FilePath = filePath;
        }
    }
}