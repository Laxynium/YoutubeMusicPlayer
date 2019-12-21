using System.Collections.Generic;
using YoutubeMusicPlayer.Framework.Messaging;

namespace YoutubeMusicPlayer.MusicBrowsing
{
    public class SearchSongQuery : IQuery<IReadOnlyList<SongDto>>
    {
        public string MusicTitle { get; }

        public SearchSongQuery(string musicTitle)
        {
            MusicTitle = musicTitle;
        }
    }
}