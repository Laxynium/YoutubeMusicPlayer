using System;

namespace YoutubeMusicPlayer.Domain.MusicDownloading
{
    public class SongDto
    {
        public Guid Id { get; }
        public string YoutubeId { get; }
        public string Title { get; }
        public string ImageSource { get; }
        public string FileLocation { get; }

        public SongDto(Guid id, string youtubeId, string title, string imageSource, string fileLocation)
        {
            Id = id;
            YoutubeId = youtubeId;
            Title = title;
            ImageSource = imageSource;
            FileLocation = fileLocation;
        }
    }
}