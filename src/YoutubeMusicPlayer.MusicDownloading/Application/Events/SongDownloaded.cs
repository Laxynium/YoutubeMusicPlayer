using System;
using YoutubeMusicPlayer.Framework.Messaging;

namespace YoutubeMusicPlayer.MusicDownloading.Application.Events
{
    public class SongDownloaded : IEvent
    {
        public Guid Id { get; }
        public string YtId { get; }
        public byte[] SongData { get; }
        public string Title { get; }
        public string ThumbnailUrl { get; }

        public SongDownloaded(Guid id , string ytId, byte[] songData, string title, string thumbnailUrl)
        {
            Id = id;
            YtId = ytId;
            Title = title;
            ThumbnailUrl = thumbnailUrl;
            SongData = songData;
        }
    }
}