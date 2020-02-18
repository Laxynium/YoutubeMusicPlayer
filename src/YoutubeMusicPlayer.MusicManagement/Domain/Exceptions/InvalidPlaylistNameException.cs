using YoutubeMusicPlayer.Framework.Exceptions;

namespace YoutubeMusicPlayer.MusicManagement.Domain.Exceptions
{
    public class InvalidPlaylistNameException: DomainException
    {
        public override string Code => "invalid_playlist_name";

        public InvalidPlaylistNameException() : base($"Invalid playlist name")
        {
        }
    }
}