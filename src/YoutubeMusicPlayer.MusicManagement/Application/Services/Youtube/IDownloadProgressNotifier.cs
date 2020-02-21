using System;
using static YoutubeMusicPlayer.MusicManagement.Application.Services.Youtube.SongDownloadEvents;

namespace YoutubeMusicPlayer.MusicManagement.Application.Services.Youtube
{
    public interface IDownloadProgressNotifier
    {
        void Notify(SongDownloadEvent @event);
        IObservable<SongDownloadEvent> Events { get; }
    }
}