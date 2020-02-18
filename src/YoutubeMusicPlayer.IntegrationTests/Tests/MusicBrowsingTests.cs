using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using YoutubeMusicPlayer.MusicBrowsing;

namespace YoutubeMusicPlayer.MusicDownloading.IntegrationTests.Tests
{
    public class MusicBrowsingTests : IntegrationTest
    {
        [Fact]
        public async Task Request_to_youtube_with_given_song_title_returns_not_empty_list_of_songs()
        {
            var result = await QueryDispatcher.DispatchAsync(new SearchSongQuery("Never Go Away"));

            result.Should().NotBeEmpty("Request to youtube should return matches.");
        }
    }
}