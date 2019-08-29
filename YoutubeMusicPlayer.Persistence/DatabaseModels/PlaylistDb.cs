using System;
using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace YoutubeMusicPlayer.Persistence.DatabaseModels
{
    [Table("Playlists")]
    internal class PlaylistDb
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<PlaylistSongDb> Songs { get; set; }
    }
}