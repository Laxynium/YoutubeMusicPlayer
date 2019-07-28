using System;
using SQLite;

namespace YoutubeMusicPlayer.Persistence.DatabaseModels
{
    [Table("Songs")]
    internal class SongDb
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public string YoutubeId { get; set; }
        public string Title { get; set; }
        public string ImageSource { get; set; }
        public string FilePath { get; set; }
    }
}