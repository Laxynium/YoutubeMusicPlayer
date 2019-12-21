using System.Threading.Tasks;
using FluentAssertions;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using Xunit;
using YoutubeMusicPlayer.Framework;
using YoutubeMusicPlayer.Framework.Messaging;
using YoutubeMusicPlayer.MusicBrowsing;

namespace YoutubeMusicPlayer.MusicDownloading.IntegrationTests.Tests.MusicBrowsing
{
    public class SearchSongTests
    {
        private IQueryDispatcher Dispatcher { get; }

        public SearchSongTests()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.Options.DefaultLifestyle = Lifestyle.Singleton;
            FrameworkStartup.Initialize(container);
            MusicBrowsingStartup.Initialize(container);
            container.Verify(VerificationOption.VerifyAndDiagnose);
            Dispatcher = container.GetInstance<IQueryDispatcher>();
        }

        [Fact]
        public async Task Request_to_youtube_with_given_song_title_returns_possible_songs()
        {
            var result = await Dispatcher.DispatchAsync(new SearchSongQuery("Never Go Away"));

            result.Should().NotBeEmpty("Request to youtube should return matches.");
        }
    }
}