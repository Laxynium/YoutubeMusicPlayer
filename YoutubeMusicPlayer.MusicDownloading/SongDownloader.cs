using System;
using System.IO;
using System.Threading.Tasks;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Domain.MusicDownloading;
using YoutubeMusicPlayer.Domain.SharedKernel;

namespace YoutubeMusicPlayer.MusicDownloading
{
    public class SongDownloader : ISongDownloader
    {
        private readonly IDownloadLinkGenerator _downloadLinkGenerator;
        private readonly IDownloader _downloader;
        private readonly IFileManager _fileManager;

        public SongDownloader(IDownloadLinkGenerator downloadLinkGenerator, IDownloader downloader, IFileManager fileManager)
        {
            _downloadLinkGenerator = downloadLinkGenerator;
            _downloader = downloader;
            _fileManager = fileManager;
        }
        public event EventHandler<(string ytId, double progress)> OnDownloadProgress;

        public async Task<Stream> DownloadMusic(string youtubeId)
        {
            var downloadLink = await _downloadLinkGenerator.GenerateLinkAsync(youtubeId);

            var downloadedFileStream = await _downloader.GetStreamAsync(downloadLink, p=>OnDownloadProgress
                ?.Invoke(this, (youtubeId, p / 100D)));

            return downloadedFileStream;
        }
    }
}