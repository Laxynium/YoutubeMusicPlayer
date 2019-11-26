namespace YoutubeMusicPlayer.MusicDownloading
{
    public class MusicDownloadingOptions
    {
        public string MusicDirectory { get; }

        public MusicDownloadingOptions(string musicDirectory)
        {
            MusicDirectory = musicDirectory;
        }
    }
}