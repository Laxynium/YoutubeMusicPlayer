using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using YoutubeMusicPlayer.Framework;
using YoutubeMusicPlayer.Framework.Messaging;
using YoutubeMusicPlayer.MusicManagement.Application.Exceptions;
using YoutubeMusicPlayer.MusicManagement.Application.Services.Youtube;
using YoutubeMusicPlayer.MusicManagement.Domain;
using YoutubeMusicPlayer.MusicManagement.Domain.Entities;
using YoutubeMusicPlayer.MusicManagement.Domain.ValueObjects;
using YoutubeMusicPlayer.MusicManagement.Infrastructure.Services.Youtube;
using static YoutubeMusicPlayer.MusicManagement.Application.Services.Youtube.SongDownloadEvents;

namespace YoutubeMusicPlayer.MusicManagement.Application.Commands.Handlers
{
    public class AddSongFromYoutubeHandler : ICommandHandler<AddSongFromYoutube>
    {
        private readonly IMainPlaylistRepository _mainPlaylistRepository;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly FileManager _fileManager;
        private readonly IYoutubeService _youtubeService;
        private readonly IDownloadProgressNotifier _downloadProgressNotifier;
        private readonly MusicManagementOptions _options;

        public AddSongFromYoutubeHandler(
            IMainPlaylistRepository mainPlaylistRepository, 
            IEventDispatcher eventDispatcher,
            FileManager fileManager,
            IYoutubeService youtubeService,
            IDownloadProgressNotifier downloadProgressNotifier,
            MusicManagementOptions options)
        {
            _mainPlaylistRepository = mainPlaylistRepository;
            _eventDispatcher = eventDispatcher;
            _fileManager = fileManager;
            _youtubeService = youtubeService;
            _downloadProgressNotifier = downloadProgressNotifier;
            _options = options;
        }
        public async Task HandleAsync(AddSongFromYoutube command)
        {
            _downloadProgressNotifier.Notify(new SongDownloadStarted(command.SongId, command.Title, command.ThumbnailUrl));
            var (_, failed, value, e) = await _youtubeService.DownloadSong(command.YtId,
                p => _downloadProgressNotifier.Notify(new SongDownloadProgressed(command.SongId,p)));

            if (failed)
            {
                _downloadProgressNotifier.Notify(new SongDownloadFailed(command.SongId));
                throw new DownloadFailedException(e);
            }
            _downloadProgressNotifier.Notify(new SongDownloadFinished(command.SongId));

            var mainPlaylist = await _mainPlaylistRepository.GetAsync();

            var songPath = SongPath.Create(command.Title);

            var (_, isFailure, error) = await _fileManager.CreateAsync(Path.Combine(_options.MusicDirectory, songPath.Value), value.Data);
            
            if(isFailure)
                throw new FileSavingFailedException(error);

            var song = new Song(new SongId(command.SongId), command.YtId, command.Title, command.Artist, songPath, command.ThumbnailUrl);
            mainPlaylist.AddSong(song);

            await _mainPlaylistRepository.UpdateAsync(mainPlaylist);
            await _eventDispatcher.DispatchAsync(mainPlaylist.Events.ToArray());
        }
    }
}