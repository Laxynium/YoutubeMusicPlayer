using System;

namespace YoutubeMusicPlayer.MusicPlaying
{
    public interface INotifyProgressChanged
    {
        event EventHandler<int> ProgressChanged;

        void OnProgressChanged(int value);
    }
}
