using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using YoutubeMusicPlayer.MusicManagement.Application.Services.Youtube;

namespace YoutubeMusicPlayer.MusicManagement.Infrastructure.Services.Youtube
{
    internal sealed class YoutubeService : IYoutubeService
    {
        private readonly YtMp3DownloadLinkGenerator _downloadLinkGenerator;
        private readonly Downloader _downloader;

        public YoutubeService(
            YtMp3DownloadLinkGenerator downloadLinkGenerator,
            Downloader downloader)
        {
            _downloadLinkGenerator = downloadLinkGenerator;
            _downloader = downloader;
        }
        public async Task<Result<YoutubeSongDto>> DownloadSong(string ytId, Action<int> onProgress)
        {
            var downloadLink = await _downloadLinkGenerator.GenerateLinkAsync(ytId);
            var data = await _downloader.GetStreamAsync(downloadLink, onProgress);
            return data.Bind(bytes=>Result.Ok(new YoutubeSongDto(ytId,bytes)));
        }
    }
}