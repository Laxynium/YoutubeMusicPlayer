using System;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace YoutubeMusicPlayer.Persistence.DatabaseModels
{
    [Table("PlaylistSongs")]
    internal class PlaylistSongDb
    {
        [PrimaryKey]
        public string Id { get; set; }

        [ForeignKey(typeof(SongDb), Unique = false)]
        public Guid SongId { get; set; }

        [ForeignKey(typeof(PlaylistDb), Unique = false)]
        public Guid PlaylistId { get; set; }
        public int Position { get; set; }
    }
}