using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using YoutubeMusicPlayer.Framework.Messaging;
using YoutubeMusicPlayer.MusicManagement.Application.Queries;
using YoutubeMusicPlayer.MusicManagement.Application.Services.Youtube;
using static YoutubeMusicPlayer.MusicManagement.Application.Services.Youtube.SongDownloadEvents;

namespace YoutubeMusicPlayer.MusicDownloading.UI
{
    public class SongDownload
    {
        public Guid SongId { get; }
        public string Title { get; }
        public string ThumbnailUrl { get; }
        public int Progress { get; }
        public Status Status { get; }

        public SongDownload(Guid songId)
        {
            SongId = songId;
        }
        public SongDownload(Guid songId, string title, string thumbnailUrl, int progress, Status status)
        {
            SongId = songId;
            Title = title;
            ThumbnailUrl = thumbnailUrl;
            Progress = progress;
            Status = status;
        }
    }
    public enum Status
    {
        InProgress,
        Completed,
        Failure
    }
    //public class SongDownloadState
    //{


    //    public SongDownloadState(int progress, Status status)
    //    {
    //        Progress = progress;
    //        Status = status;
    //    }
    //}
    public class SongDownloadsStore
    {
        private readonly IQueryDispatcher _queryDispatcher;

        private IObservable<List<IObservable<SongDownload>>> _songs;
        private List<IObservable<SongDownload>> _songs2;
        public void Subscribe(Action<SongDownload> onNewSong, Action<SongDownload> onSongUpdate)
        {
            //_songs.Subscribe(x=>x.)
            //_songs.Subscribe(x => onSongUpdate(x));
            //_songs.Subscribe(x => x.Subscribe(onSongUpdate));
            //_songs.Subscribe(
            //    x =>
            //    {
            //        onNewSong?.Invoke(x);
            //        x.Subscribe(s => onSongUpdate(x));
            //    }
            //);
        }

        public SongDownloadsStore(IDownloadProgressNotifier notifier, IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;

            var songs = notifier.Events
                .GroupBy(x => x.SongId)
                .Select(
                    x => new List<IObservable<SongDownload>>
                    {
                        x.Scan(
                            (SongDownload) null,
                            (sd, e) => e switch
                            {
                                SongDownloadStarted s => new SongDownload(s.SongId, s.Title, s.ThumbnailUrl, 0, Status.InProgress),
                                SongDownloadProgressed s => new SongDownload(
                                    s.SongId,
                                    sd.Title,
                                    sd.ThumbnailUrl,
                                    s.Progress,
                                    Status.InProgress
                                ),
                                SongDownloadFailed s => new SongDownload(s.SongId, sd.Title, sd.ThumbnailUrl, sd.Progress, Status.Failure),
                                SongDownloadFinished s => new SongDownload(s.SongId, sd.Title, sd.ThumbnailUrl, 100, Status.Completed)
                            }
                        )
                    }
                );
                //.SelectMany(x => Observable.Start(()=>x.Key));
                //.Select(x=>new List<IObservable<SongDownload>>() as IEnumerable<IObservable<SongDownload>>)
                //.Select(
                //    x => x.Scan(
                //        (SongDownload)null,
                //        (sd, e) => e switch
                //        {
                //            SongDownloadStarted s => new SongDownload(s.SongId, s.Title, s.ThumbnailUrl, 0, Status.InProgress),
                //            SongDownloadProgressed s => new SongDownload(s.SongId, sd.Title, sd.ThumbnailUrl, s.Progress, Status.InProgress),
                //            SongDownloadFailed s => new SongDownload(s.SongId, sd.Title, sd.ThumbnailUrl, sd.Progress, Status.Failure),
                //            SongDownloadFinished s => new SongDownload(s.SongId, sd.Title, sd.ThumbnailUrl, 100, Status.Completed)
                //        }
                //    )
                //);
            _songs = songs;
        }

        public async Task Initialize()
        {
            var songs = await _queryDispatcher.DispatchAsync(new GetAllSongsFromYoutube());

            //var newSongs = new ReplaySubject<(SongDownload song, ISubject<SongDownloadState> state)>();
            //newSongs.Subscribe(_songs);
            //_songs = newSongs;
            //songs.Where(s=>_songs.All(x=>x.song.SongId != s.SongId).Wait()).ToList()
            //    .ForEach(
            //        s =>
            //        {
            //            var state = new ReplaySubject<SongDownloadState>();
            //            _songs.OnNext((new SongDownload(s.SongId, s.Title, s.ThumbnailUrl), state));
            //            state.OnNext(new SongDownloadState(100, Status.Completed));
            //        });
            //songs.ForEach(
            //    s =>
            //    {
            //        var state = new ReplaySubject<SongDownloadState>();
            //        _songs.OnNext((new SongDownload(s.SongId,s.Title,s.ThumbnailUrl), state));
            //        state.OnNext(new SongDownloadState(100, Status.Completed));
            //    });
        }
    }
}