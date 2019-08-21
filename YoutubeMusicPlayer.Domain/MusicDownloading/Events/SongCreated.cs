using System;
using YoutubeMusicPlayer.Domain.Framework;

namespace YoutubeMusicPlayer.Domain.MusicDownloading.Events
{
    public class SongCreated : IEvent
    {
        public Guid Id { get; }
        public string YoutubeId { get; }
        public string FilePath { get; }
        public string Title { get; }
        public string ImageSource { get; }

        public SongCreated(Guid id , string youtubeId, string filePath, string title, string imageSource)
        {
            Id = id;
            YoutubeId = youtubeId;
            Title = title;
            ImageSource = imageSource;
            FilePath = filePath;
        }
    }
}