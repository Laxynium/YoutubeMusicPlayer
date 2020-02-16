using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoutubeMusicPlayer.Framework.BuildingBlocks;

namespace YoutubeMusicPlayer.MusicManagement.Domain
{
    public class Playlist : Entity<PlaylistId>
    {
        public PlaylistName Name { get; private set; }
        private readonly IList<SongId> _songs = new List<SongId>();
        public IReadOnlyList<SongId> Songs => _songs.ToList();
        public bool IsMaster { get; private set; }
        private Playlist():base(new PlaylistId(Guid.NewGuid()))
        {

        }

        private Playlist(PlaylistId id, PlaylistName name, bool isMaster) : base(id)
        {
            Name = name;
            IsMaster = isMaster;
        }

        public static async Task<Playlist> CreateMaster(PlaylistName name, IUniqueMasterListRule rule)
        {
            if(! await rule.IsUnique())
                throw new Exception("Only one master list can exists.");
            return new Playlist(new PlaylistId(Guid.NewGuid()), name, true);
        }
        public static Playlist CreateNormal(PlaylistName name)
        {
            return new Playlist(new PlaylistId(Guid.NewGuid()), name,false);
        }
        public void ChangeName(PlaylistName name)
        {
            Name = name;
        }

        public void AddSong(SongId songId)
        {
            _songs.Add(songId);
        }

        public void RemoveSong(SongPosition position)
        {
            _songs.RemoveAt(position);
        }

        public void ChangePosition(SongPosition oldPosition, SongPosition newPosition)
        {
            if(!oldPosition.InRange(_songs.Count) || !newPosition.InRange(_songs.Count))
                throw new Exception($"New or old position is out of range.");

            var songId = Songs[oldPosition.Value];

            _songs.RemoveAt(oldPosition);

            _songs.Insert(newPosition, songId);
        }
    }
}