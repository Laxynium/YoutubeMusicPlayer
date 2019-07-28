using System;

namespace YoutubeMusicPlayer.Domain.MusicManagement.Queries
{
    public class SongDto
    {
        public Guid SongId { get; set; }
        public string Title { get; set; }
        public string ImageSource { get; set; }
    }
}