using YoutubeMusicPlayer.Framework.Exceptions;

namespace YoutubeMusicPlayer.MusicManagement.Domain.Exceptions
{
    public class SongAlreadyOnMainPlaylistException : DomainException
    {
        public override string Code => "song_already_on_main_playlist";

        public SongAlreadyOnMainPlaylistException(string songTitle) : base($"Song: '{songTitle}' is already on main playlist.")
        {
        }
    }
}