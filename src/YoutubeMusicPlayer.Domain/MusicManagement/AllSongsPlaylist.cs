using System.Collections.Generic;
using YoutubeMusicPlayer.Domain.SharedKernel;

namespace YoutubeMusicPlayer.Domain.MusicManagement
{
    public class AllSongsPlaylist : PlaylistBase
    {
        public AllSongsPlaylist(PlaylistId id, PlaylistName name, IEnumerable<Song> songs = null) : base(id, name, songs)
        {
        }
    }

}