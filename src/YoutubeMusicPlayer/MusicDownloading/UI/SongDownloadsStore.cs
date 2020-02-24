using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;
using YoutubeMusicPlayer.Framework.Messaging;
using YoutubeMusicPlayer.MusicManagement.Application.Queries;
using YoutubeMusicPlayer.MusicManagement.Application.Services.Youtube;
using static YoutubeMusicPlayer.MusicManagement.Application.Services.Youtube.SongDownloadEvents;

namespace YoutubeMusicPlayer.MusicDownloading.UI
{
    public class SongDownload
    {
        public Guid SongId { get; set; }
        public string Title { get; set; }
        public string ThumbnailUrl { get; set; }
        public int Progress { get; set; }
        public Status Status { get; set; }

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

        public SongDownload Update(string title = null, string thumbnailUrl = null, int? progress = null,
            Status? status = null)
        {
            return new SongDownload(SongId, title ?? Title, thumbnailUrl ?? ThumbnailUrl, progress ?? Progress,
                status ?? Status);
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

        //private readonly List<SongDownload> _songList = new List<SongDownload>();
        private readonly BehaviorSubject<List<SongDownload>> _songs;
        public IObservable<List<SongDownload>> SongDownloads => _songs.AsObservable();

        public SongDownloadsStore(IDownloadProgressNotifier notifier, IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;

            _songs = new BehaviorSubject<List<SongDownload>>(new List<SongDownload>());
            notifier.Events
                .GroupBy(x => x.SongId)
                .SelectMany(x =>
                {
                    return x.Scan(
                        (SongDownload) null,
                        (sd, e) => e switch
                        {
                            SongDownloadStarted s => new SongDownload(s.SongId, s.Title, s.ThumbnailUrl, 0,
                                Status.InProgress),
                            SongDownloadProgressed s => new SongDownload(s.SongId, sd.Title, sd.ThumbnailUrl,
                                s.Progress, Status.InProgress),
                            SongDownloadFailed s => new SongDownload(s.SongId, sd.Title, sd.ThumbnailUrl, sd.Progress,
                                Status.Failure),
                            SongDownloadFinished s => new SongDownload(s.SongId, sd.Title, sd.ThumbnailUrl, 100,
                                Status.Completed)
                        }
                    );
                })
                .Subscribe(AddOrReplace);
        }

        public void AddOrReplace(SongDownload x)
        {
            var songs = _songs.Value.ToList();
            var song = songs.Find(y => y.SongId == x.SongId);
            if (song is null)
                songs.Add(x);
            else
            {
                var index = songs.IndexOf(song);
                songs[index] = x;
            }

            _songs.OnNext(songs);
        }

        public async Task Initialize()
        {
            var songs = await _queryDispatcher.DispatchAsync(new GetAllSongsFromYoutube());

            songs
                .Select(s => new SongDownload(s.SongId, s.Title, s.ThumbnailUrl, 100, Status.Completed))
                .ForEach(AddOrReplace);
            //_songList.Clear();
            //songs.Select(s => 
            //        new SongDownload(s.SongId, s.Title, s.ThumbnailUrl, 100, Status.Completed))
            //    .ForEach(_songList.Add);

            //_songs.OnNext(_songList.ToList());
        }
    }
}