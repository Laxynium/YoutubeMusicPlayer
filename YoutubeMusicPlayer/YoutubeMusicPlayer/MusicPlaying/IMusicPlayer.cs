﻿using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using YoutubeMusicPlayer.MusicPlaying;

[assembly: Dependency(typeof(IMusicPlayer))]
namespace YoutubeMusicPlayer.MusicPlaying
{
    public interface IMusicPlayer
    {
        event EventHandler PlaybackCompleted;
        event EventHandler<int> ProgressChanged;

        Task SetProgressAsync(double percentOfLength);
        Task SetSourceAsync(string fileUrl);
        Task PlayAsync();
        Task Pause();
        Task Stop();
    }
}