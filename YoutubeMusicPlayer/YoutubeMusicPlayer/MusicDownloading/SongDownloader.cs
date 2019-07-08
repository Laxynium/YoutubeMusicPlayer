using System;
using System.Threading.Tasks;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Domain.MusicDownloading;

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

        public async Task<SongPath> DownloadMusic(string youtubeId, string title)
        {
            var downloadLink = await _downloadLinkGenerator.GenerateLinkAsync(youtubeId);

            var downloadedFileStream = await _downloader.GetStreamAsync(downloadLink, p=>OnDownloadProgress
                ?.Invoke(this, (youtubeId, p / 100D)));

            var filePath = _fileManager.GeneratePath(title);

            if (_fileManager.Exists(filePath))
            {
                await _fileManager.DeleteFileAsync(filePath);
            }

            await _fileManager.CreateFileAsync(title, downloadedFileStream);

            return new SongPath(filePath);
        }
    }
}