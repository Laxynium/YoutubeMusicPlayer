using System;

namespace YoutubeMusicPlayer.MusicPlaying
{
    public interface INotifyProgressChanged
    {
        void OnProgressChanged(int value);
    }
}
