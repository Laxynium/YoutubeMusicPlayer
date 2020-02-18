using YoutubeMusicPlayer.Framework.Exceptions;

namespace YoutubeMusicPlayer.MusicManagement.Application.Exceptions
{
    public class FileSavingFailedException : AppException
    {
        public override string Code => "file_saving_failed";
        public string Reason { get; }
        public FileSavingFailedException(string reason) : base("Fail saving failed")
        {
            Reason = reason;
        }
    }
}