using YoutubeMusicPlayer.MusicDownloading.ViewModels;

namespace YoutubeMusicPlayer.Framework.MessangingCenter
{
    public class MusicEventArgs:System.EventArgs
    {
        public MusicViewModel Music { get; set; }
    }
}
