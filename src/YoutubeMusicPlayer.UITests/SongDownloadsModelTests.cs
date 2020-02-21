using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using YoutubeMusicPlayer.Framework.Messaging;
using YoutubeMusicPlayer.MusicDownloading.UI;
using YoutubeMusicPlayer.MusicManagement.Application.Queries;
using YoutubeMusicPlayer.MusicManagement.Infrastructure.Services.Youtube;
using static YoutubeMusicPlayer.MusicManagement.Application.Services.Youtube.SongDownloadEvents;

namespace YoutubeMusicPlayer.UITests
{
    public class SongDownloadsModelTests
    {
        [Fact]
        public void Learning_how_rx_works()
        {
            var obs = new ReplaySubject<List<(int, string)>>();

            var list = new List<(int,string)>();
            obs.Subscribe(
                x =>
                {
                    foreach (var el in x)
                    {
                        list.Add(el);
                        //Console.WriteLine($"{id}->{value}");
                    }
                }
            );

            obs.OnNext(new List<(int, string)>{(1,"a")});
            obs.OnNext(new List<(int, string)>{(2,"b")});

            var list2 = new List<(int, string)>();
            obs.Subscribe(
                x =>
                {
                    foreach (var el in x)
                    {
                        list2.Add(el);
                        //Console.WriteLine($"{id}->{value}");
                    }
                }
            );
        }

        [Fact]
        public void Test()
        {
            var notifier = new DownloadProgressNotifier();
            var model = new SongDownloadsStore(notifier, null);

            var songs = new List<SongDownload>();
            var songStates = new List<SongDownload>();
            model.Subscribe(
                s =>
                {
                },
                s =>
                {
                    songs.Add(s);
                });

            var songId = Guid.NewGuid();
            var title = "title";
            var thumbnailUrl = "thumbnail";

            notifier.Notify(new SongDownloadStarted(songId, title, thumbnailUrl));

            notifier.Notify(new SongDownloadProgressed(songId, 10));
            notifier.Notify(new SongDownloadProgressed(songId, 50));
            notifier.Notify(new SongDownloadProgressed(songId, 100));

            notifier.Notify(new SongDownloadFinished(songId));

            //songs.Should().HaveCount(5)
            //    .And
            //    .ContainEquivalentOf(new SongDownload(songId, title, thumbnailUrl));
            //songStates.Should().HaveCount(4)
            //    .And.BeEquivalentTo(
            //        new List<(SongDownload, SongDownloadState)>
            //        {
            //            (songs[0], new SongDownloadState( 10, Status.InProgress)),
            //            (songs[0], new SongDownloadState( 50, Status.InProgress)),
            //            (songs[0], new SongDownloadState( 100, Status.InProgress)),
            //            (songs[0], new SongDownloadState( 100, Status.Completed))
            //        }
            //    );
        }

        //[Fact]
        //public void Test2()
        //{
        //    var notifier = new DownloadProgressNotifier();
        //    var model = new SongDownloadsStore(notifier, null);

        //    var songs = new List<SongDownload>();
        //    var songStates = new List<(SongDownload, SongDownloadState)>();
        //    model.Subscribe(
        //        s =>
        //        {
        //            songs.Add(s);
        //        },
        //        (s,ss) =>
        //        {
        //            songStates.Add((s,ss));
        //        });

        //    var songId = Guid.NewGuid();
        //    var title = "title";
        //    var thumbnailUrl = "thumbnail";

        //    notifier.Notify(new SongDownloadStarted(songId, title, thumbnailUrl));

        //    notifier.Notify(new SongDownloadProgressed(songId, 10));
        //    notifier.Notify(new SongDownloadProgressed(songId, 50));
        //    notifier.Notify(new SongDownloadFailed(songId));
        //    notifier.Notify(new SongDownloadProgressed(songId, 90));
        //    notifier.Notify(new SongDownloadProgressed(songId, 100));

        //    notifier.Notify(new SongDownloadFinished(songId));

        //    songs.Should().HaveCount(1)
        //        .And
        //        .ContainEquivalentOf(new SongDownload(songId, title, thumbnailUrl));
        //    songStates.Should().HaveCount(3)
        //        .And.BeEquivalentTo(
        //            new List<(SongDownload, SongDownloadState)>
        //            {
        //                (songs[0],new SongDownloadState( 10, Status.InProgress)),
        //                (songs[0],new SongDownloadState( 50, Status.InProgress)),
        //                (songs[0],new SongDownloadState( -1, Status.Failure))
        //            }
        //        );
        //}

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
