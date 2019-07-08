using System;

namespace YoutubeMusicPlayer.Domain.MusicDownloading
{
    public class MusicDto
    {
        public Guid Id { get; }
        public string YoutubeId { get; }
        public string Title { get; }
        public string ImageSource { get; }
        public string FilePath { get; }

        public MusicDto(Guid id, string youtubeId, string title, string imageSource, string filePath)
        {
            Id = id;
            YoutubeId = youtubeId;
            Title = title;
            ImageSource = imageSource;
            FilePath = filePath;
        }
    }
}