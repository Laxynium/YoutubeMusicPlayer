using System;
using System.IO;
using System.Threading.Tasks;
using System.Transactions;
using ChinhDo.Transactions.FileManager;
using CSharpFunctionalExtensions;
using YoutubeMusicPlayer.Framework;
using YoutubeMusicPlayer.MusicDownloading.Domain;
using YoutubeMusicPlayer.MusicDownloading.Events;
using YoutubeMusicPlayer.MusicDownloading.Repositories;

namespace YoutubeMusicPlayer.MusicDownloading.Commands.Download
{
    internal class DownloadSongCommandHandler : ICommandHandler<DownloadSongCommand>
    {
        private readonly ISongRepository _songRepository;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly YtMp3DownloadLinkGenerator _downloadLinkGenerator;
        private readonly Downloader _downloader;
        private readonly IFileManager _fileManager;
        private readonly MusicDownloadingOptions _options;

        public DownloadSongCommandHandler(ISongRepository songRepository, 
            IEventDispatcher eventDispatcher, 
            YtMp3DownloadLinkGenerator downloadLinkGenerator, 
            Downloader downloader, 
            IFileManager fileManager,
            MusicDownloadingOptions options)
        {
            _songRepository = songRepository;
            _eventDispatcher = eventDispatcher;
            _downloadLinkGenerator = downloadLinkGenerator;
            _downloader = downloader;
            _fileManager = fileManager;
            _options = options;
        }
        public async Task HandleAsync(DownloadSongCommand command)
        {
            using (var transaction = new TransactionScope())
            { 
                if (await _songRepository.Exists(command.YtId))
                    return;

                await _eventDispatcher.DispatchAsync(new DownloadStarted(command.YtId, command.SongTitle, command.ImageSource));

                var (_, isFailure, value, error) = await DownloadMusic(command.YtId);

                if (isFailure)
                {
                    await _eventDispatcher.DispatchAsync(new DownloadFailed(command.YtId, error.msg));

                    transaction.Complete();
                    return;
                }

                var filePath = await SaveFile(command.SongTitle, value);

                var song = new Song(filePath, command.YtId, command.SongTitle, command.ImageSource);

                await _songRepository.AddAsync(song);

                await _eventDispatcher.DispatchAsync(new DownloadFinished(song.Id, song.YtId, song.FilePath, song.Title, song.ImageSource));

                transaction.Complete();
            }

        }

        private async Task<Result<byte[],(string msg, Exception e)>> DownloadMusic(string ytId)
        {
            var downloadLink = await _downloadLinkGenerator.GenerateLinkAsync(ytId);

            async void OnProgress(double progress) => await _eventDispatcher.DispatchAsync(new DownloadProgressed(ytId, progress));
            var downloadedFileStream = await _downloader.GetStreamAsync(downloadLink, 
                p => OnProgress(p / 100D));

            return downloadedFileStream;
        }

        private async Task<string> SaveFile(string title, byte[] fileStream)
        {
            var fileName = title.Replace(" ", "_").Replace(":", "_");
            var savePath = Path.Combine(_options.MusicDirectory, fileName+".mp3");

            if (_fileManager.FileExists(savePath))
                return savePath;
            _fileManager.WriteAllBytes(savePath, fileStream);
            await Task.CompletedTask;
            return savePath;
            //var filePath = _fileManager.GeneratePath(title);

            //if (_fileManager.Exists(filePath))
            //{
            //    await _fileManager.DeleteFileAsync(filePath);
            //}

            //await _fileManager.CreateFileAsync(title, fileStream);

            //return new SongPath(filePath);
        }
    }
}