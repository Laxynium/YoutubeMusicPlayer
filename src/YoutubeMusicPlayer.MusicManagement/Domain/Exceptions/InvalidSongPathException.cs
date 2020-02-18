using YoutubeMusicPlayer.Framework.Exceptions;

namespace YoutubeMusicPlayer.MusicManagement.Domain.Exceptions
{
    public class InvalidSongPathException : DomainException
    {
        public override string Code => "invalid_song_path";

        public InvalidSongPathException() : base($"Invalid song path")
        {
        }
    }
}