using System;
using YoutubeMusicPlayer.Framework.Messaging;

namespace YoutubeMusicPlayer.MusicDownloading.Application.Commands
{
    public class DownloadSongCommand : ICommand
    {
        public Guid SongId { get; }
        public string YtId { get; }
        public string SongTitle { get; }
        public string ImageSource { get; }

        public DownloadSongCommand(Guid songId, string ytId, string songTitle, string imageSource)
        {
            this.SongId = songId;
            YtId = ytId;
            SongTitle = songTitle;
            ImageSource = imageSource;
        }
    }
}