using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using YoutubeMusicPlayer.MusicManagement.Application.Services.Youtube;

namespace YoutubeMusicPlayer.MusicManagement.Infrastructure.Services.Youtube
{
    public class DownloadProgressNotifier : IDownloadProgressNotifier
    {
        private readonly ISubject<SongDownloadEvents.SongDownloadEvent> _events = new ReplaySubject<SongDownloadEvents.SongDownloadEvent>();
        public void Notify(SongDownloadEvents.SongDownloadEvent @event)
        {
            _events.OnNext(@event);
        }

        public IObservable<SongDownloadEvents.SongDownloadEvent> Events => _events.AsObservable();
    }
}