using System;
using YoutubeMusicPlayer.Domain.Framework;

namespace YoutubeMusicPlayer.Domain.MusicDownloading
{
    public class MusicDownloaded : IEvent
    {
        public Guid Id { get; }
        public string Path { get; }
        public string ImageSource { get; }

        public MusicDownloaded(Guid id, string path, string imageSource)
        {
            Id = id;
            Path = path;
            ImageSource = imageSource;
        }
    }
}