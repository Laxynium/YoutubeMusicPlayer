using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FluentAssertions;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using Xunit;
using YoutubeMusicPlayer.Framework;
using YoutubeMusicPlayer.MusicDownloading.Commands;

namespace YoutubeMusicPlayer.MusicDownloading.Tests
{
    public class DownloadMusicIntegrationTests
    {
        [Fact]
        public async Task Operation_is_successful_when_no_problems_with_download()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            MusicDownloadingStartup.Initialize(container,"TestDb","music");

            container.Verify(VerificationOption.VerifyAndDiagnose);

            var dispatcher = container.GetInstance<ICommandDispatcher>();

            //setup database
            //setup IoC container

            var command = new DownloadSongCommand("dqVZaN4lnwQ", "Never Go Away", "source.com/img");

            await dispatcher.DispatchAsync(command);

            //result.Should().Be(Result.Ok());
        }
    }
}
