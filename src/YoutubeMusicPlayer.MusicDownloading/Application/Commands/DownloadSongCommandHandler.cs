using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Transactions;
using ChinhDo.Transactions.FileManager;
using CSharpFunctionalExtensions;
using YoutubeMusicPlayer.Framework;
using YoutubeMusicPlayer.Framework.Messaging;
using YoutubeMusicPlayer.MusicDownloading.Application.Events;
using YoutubeMusicPlayer.MusicDownloading.Application.Repositories;
using YoutubeMusicPlayer.MusicDownloading.Application.Services;
using YoutubeMusicPlayer.MusicDownloading.Domain;

namespace YoutubeMusicPlayer.MusicDownloading.Application.Commands
{
    internal class DownloadSongCommandHandler : ICommandHandler<DownloadSongCommand>
    {
        private readonly ISongRepository _songRepository;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly YtMp3DownloadLinkGenerator _downloadLinkGenerator;
        private readonly Downloader _downloader;
        private readonly FileManager _fileManager;
        private readonly MusicDownloadingOptions _options;

        public DownloadSongCommandHandler(
            MusicDownloadingOptions options, 
            ISongRepository songRepository, 
            IEventDispatcher eventDispatcher, 
            FileManager fileManager, 
            YtMp3DownloadLinkGenerator downloadLinkGenerator,
            Downloader downloader)
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
            if (await _songRepository.Exists(command.YtId))
                return;

            await _eventDispatcher.DispatchAsync(new DownloadStarted(command.YtId, command.SongTitle, command.ImageSource));

            var (_, isFailure, bytes, error) = await DownloadMusic(command.YtId);
            if (isFailure)
            {
                await _eventDispatcher.DispatchAsync(new DownloadFailed(command.YtId, error));
                return;
            }
            var filePath = await SaveFile(command.SongTitle, bytes);

            var song = new Song(command.SongId, filePath, command.YtId, command.SongTitle, command.ImageSource);

            await _songRepository.AddAsync(song);

            await _eventDispatcher.DispatchAsync(new DownloadFinished(song.Id, song.YtId, song.FilePath, song.Title, song.ImageSource));
        }

        private async Task<Result<byte[]>> DownloadMusic(string ytId)
        {
            var downloadLink = await _downloadLinkGenerator.GenerateLinkAsync(ytId);
            async void OnProgress(double progress) => await _eventDispatcher.DispatchAsync(new DownloadProgressed(ytId, progress));

            return await _downloader.GetStreamAsync(downloadLink, p => OnProgress(p / 100D));
        }

        private async Task<string> SaveFile(string title, byte[] fileStream)
        {
            var fileName = title.Replace(" ", "_").Replace(":", "_");
            var savePath = Path.Combine(_options.MusicDirectory, fileName+".mp3");

            if (File.Exists(savePath))
                return savePath;
            await _fileManager.CreateAsync(savePath, fileStream);
            return savePath;
        }
    }
}