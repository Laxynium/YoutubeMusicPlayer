using YoutubeMusicPlayer.Framework.Messaging;

namespace YoutubeMusicPlayer.MusicDownloading.Application.Commands
{
    public class DownloadSongCommand : ICommand
    {
        public string YtId { get; }
        public string SongTitle { get; }
        public string ImageSource { get; }

        public DownloadSongCommand(string ytId, string songTitle, string imageSource)
        {
            YtId = ytId;
            SongTitle = songTitle;
            ImageSource = imageSource;
        }
    }
}