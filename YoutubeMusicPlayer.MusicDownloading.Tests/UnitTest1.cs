using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FluentAssertions;
using Xunit;
using YoutubeMusicPlayer.MusicDownloading.Commands.Download;

namespace YoutubeMusicPlayer.MusicDownloading.Tests
{
    public class DownloadMusicIntegrationTests
    {
        [Fact]
        public async Task Operation_is_successful_when_no_problems_with_download()
        {
            //setup database
            //setup IoC container

            var command = new DownloadSongCommand("dqVZaN4lnwQ", "Never Go Away", "source.com/img");
            var commandDispatcher = new CommandDispatcher();

            var result = await commandDispatcher.DisplatchAsync(command);


            result.Should().Be(Result.Ok());
        }
    }
}
