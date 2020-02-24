using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;
using YoutubeMusicPlayer.MusicDownloading.UI;
using YoutubeMusicPlayer.MusicManagement.Infrastructure.Services.Youtube;
using static YoutubeMusicPlayer.MusicManagement.Application.Services.Youtube.SongDownloadEvents;

namespace YoutubeMusicPlayer.UITests
{
    public class SongDownloadsModelTests
    {
        [Fact]
        public void Test()
        {
            var notifier = new DownloadProgressNotifier();
            var model = new SongDownloadsStore(notifier, null);

            var songs = new List<List<SongDownload>>();
            model.SongDownloads.Subscribe(songDownloads => { songs.Add(songDownloads.ToList()); });

            var songId = Guid.NewGuid();
            var title = "title";
            var thumbnailUrl = "thumbnail";

            notifier.Notify(new SongDownloadStarted(songId, title, thumbnailUrl));

            notifier.Notify(new SongDownloadProgressed(songId, 10));
            notifier.Notify(new SongDownloadProgressed(songId, 100));

            notifier.Notify(new SongDownloadFinished(songId));

            songs.Should().HaveCount(5)
                .And.BeEquivalentTo(new List<List<SongDownload>>
                {
                    new List<SongDownload>(),
                    new List<SongDownload> {new SongDownload(songId, title, thumbnailUrl, 0, Status.InProgress)},
                    new List<SongDownload> {new SongDownload(songId, title, thumbnailUrl, 10, Status.InProgress)},
                    new List<SongDownload> {new SongDownload(songId, title, thumbnailUrl, 100, Status.InProgress)},
                    new List<SongDownload> {new SongDownload(songId, title, thumbnailUrl, 100, Status.Completed)},
                });
        }

        //class FakeQueryDispatcher : IQueryDispatcher
        //{
        //    private readonly List<SongDto> _songs;

        //    public FakeQueryDispatcher(List<SongDto>songs)
        //    {
        //        _songs = songs;
        //    }
        //    public Task<TResult> DispatchAsync<TResult>(IQuery<TResult> query) =>
        //        Task.FromResult((TResult)_songs.AsEnumerable());
        //}

        //[Fact]
        //public async Task Test3()
        //{
        //    var songDto1 = new SongDto(Guid.NewGuid(), "yt1","title1","thumb1");
        //    var songDto2 = new SongDto(Guid.NewGuid(), "yt2","title2","thumb2");
        //    var songDto3 = new SongDto(Guid.NewGuid(), "yt3","title3","thumb3");
        //    var notifier = new DownloadProgressNotifier();
        //    var dispatcher = new FakeQueryDispatcher(new List<SongDto>{ songDto1, songDto2, songDto3 });
        //    var model = new SongDownloadsStore(notifier, dispatcher);
        //    await model.Initialize();


        //    var songs = new List<SongDownload>();
        //    var songStates = new List<(SongDownload, SongDownloadState)>();
        //    model.Subscribe(
        //        s =>
        //        {
        //            songs.Add(s);
        //        },
        //        (s, ss) =>
        //        {
        //            songStates.Add((s, ss));
        //        });
        //    songs.Should().BeEquivalentTo(
        //        new List<SongDownload>
        //        {
        //            new SongDownload(songDto1.SongId,songDto1.Title,songDto1.ThumbnailUrl),
        //            new SongDownload(songDto2.SongId,songDto2.Title,songDto2.ThumbnailUrl),
        //            new SongDownload(songDto3.SongId,songDto3.Title,songDto3.ThumbnailUrl)
        //        }
        //    );
        //    songStates.Should().BeEquivalentTo(
        //            new List<(SongDownload, SongDownloadState)>
        //            {
        //                (songs[0],new SongDownloadState( 100, Status.Completed)),
        //                (songs[1],new SongDownloadState( 100, Status.Completed)),
        //                (songs[2],new SongDownloadState( 100, Status.Completed))
        //            }
        //        );
        //}

        //[Fact]
        //public async Task Test4()
        //{
        //    var songDto1 = new SongDto(Guid.NewGuid(), "yt1", "title1", "thumb1");
        //    var songDto2 = new SongDto(Guid.NewGuid(), "yt2", "title2", "thumb2");
        //    var songDto3 = new SongDto(Guid.NewGuid(), "yt3", "title3", "thumb3");
        //    var notifier = new DownloadProgressNotifier();
        //    var dispatcher = new FakeQueryDispatcher(new List<SongDto> { songDto1, songDto2, songDto3 });
        //    var model = new SongDownloadsStore(notifier, dispatcher);
        //    await model.Initialize();


        //    var songs = new List<SongDownload>();
        //    var songStates = new List<(SongDownload, SongDownloadState)>();
        //    model.Subscribe(
        //        s =>
        //        {
        //            songs.Add(s);
        //        },
        //        (s, ss) =>
        //        {
        //            songStates.Add((s, ss));
        //        });

        //    await model.Initialize();

        //    songs.Should().BeEquivalentTo(
        //        new List<SongDownload>
        //        {
        //            new SongDownload(songDto1.SongId,songDto1.Title,songDto1.ThumbnailUrl),
        //            new SongDownload(songDto2.SongId,songDto2.Title,songDto2.ThumbnailUrl),
        //            new SongDownload(songDto3.SongId,songDto3.Title,songDto3.ThumbnailUrl)
        //        }
        //    );
        //    songStates.Should().BeEquivalentTo(
        //        new List<(SongDownload, SongDownloadState)>
        //        {
        //            (songs[0],new SongDownloadState( 100, Status.Completed)),
        //            (songs[1],new SongDownloadState( 100, Status.Completed)),
        //            (songs[2],new SongDownloadState( 100, Status.Completed))
        //        }
        //    );
        //}
    }
}