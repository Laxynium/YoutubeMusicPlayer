using YoutubeMusicPlayer.ViewModels;

namespace YoutubeMusicPlayer.EventArgs
{
    public class MusicEventArgs:System.EventArgs
    {
        public MusicViewModel Music { get; set; }
    }
}
