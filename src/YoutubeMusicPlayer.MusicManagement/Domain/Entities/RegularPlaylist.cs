using System.Collections.Generic;
using System.Linq;
using YoutubeMusicPlayer.Framework.BuildingBlocks;
using YoutubeMusicPlayer.MusicManagement.Domain.ValueObjects;

namespace YoutubeMusicPlayer.MusicManagement.Domain.Entities
{
    public class RegularPlaylist : Entity<ValueObjects.PlaylistId>
    {
        public PlaylistName Name { get; }
        private readonly IList<SongId> _songs = new List<SongId>();
        public IReadOnlyList<SongId> Songs => _songs.ToList();
        public RegularPlaylist(ValueObjects.PlaylistId id, PlaylistName name) 
            : base(id)
        {
            Name = name;
        }
    }
}