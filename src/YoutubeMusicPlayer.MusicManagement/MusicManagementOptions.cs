namespace YoutubeMusicPlayer.MusicManagement
{
    public class MusicManagementOptions
    {
        public string MusicDirectory { get; }

        public MusicManagementOptions(string musicDirectory)
        {
            MusicDirectory = musicDirectory;
        }
    }
}