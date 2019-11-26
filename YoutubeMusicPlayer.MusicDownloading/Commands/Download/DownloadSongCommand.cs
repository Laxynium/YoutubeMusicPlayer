using YoutubeMusicPlayer.Framework;

namespace YoutubeMusicPlayer.MusicDownloading.Commands.Download
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