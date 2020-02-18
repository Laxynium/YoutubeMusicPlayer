using YoutubeMusicPlayer.Framework.Exceptions;

namespace YoutubeMusicPlayer.MusicManagement.Application.Exceptions
{
    public class DownloadFailedException : AppException
    {
        public override string Code => "download_failed";
        public string Reason { get; }
        public DownloadFailedException(string reason) : base("Download failed")
        {
            Reason = reason;
        }
    }
}