using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YoutubeMusicPlayer.Domain.Framework;
using YoutubeMusicPlayer.Domain.MusicDownloading;
using YoutubeMusicPlayer.Domain.MusicDownloading.Events;
using YoutubeMusicPlayer.Domain.MusicDownloading.Repositories;
using YoutubeMusicPlayer.Domain.SharedKernel;
using Song = YoutubeMusicPlayer.Domain.MusicDownloading.Song;

namespace YoutubeMusicPlayer.MusicDownloading
{
    public class SongService : ISongService
    {
        private readonly ISongDownloader _songDownloader;
        private readonly ISongRepository _songRepository;
        private readonly IFileManager _fileManager;
        private readonly IEventDispatcher _dispatcher;

        public SongService(ISongDownloader songDownloader, ISongRepository songRepository, IFileManager fileManager, IEventDispatcher dispatcher)
        {
            _songRepository = songRepository;
            _fileManager = fileManager;
            _dispatcher = dispatcher;
            _songDownloader = songDownloader;
        }

        public async Task DownloadAndCreateSong(string youtubeId, string title, string imageSource)
        {
            if (await _songRepository.Exists(youtubeId))
                return;

            await _dispatcher.DispatchAsync(new DownloadStarted(youtubeId,title,imageSource));

            async void OnProgress(object _, (string ytId, double progress) x) => await _dispatcher.DispatchAsync(new DownloadProgressed(x.ytId, x.progress));

            _songDownloader.OnDownloadProgress += OnProgress;
            try
            {
                var fileStream = await _songDownloader.DownloadMusic(youtubeId);

                var filePath = await SaveFile(title, fileStream);

                var song = new Song(filePath,youtubeId,title,imageSource);

                await _songRepository.AddAsync(song);

                await _dispatcher.DispatchAsync(song.Events.ToArray());
            }
            catch (Exception e)
            {
                await _dispatcher.DispatchAsync(new DownloadFailed(youtubeId, e.Message));
            }
            finally
            {
                _songDownloader.OnDownloadProgress -= OnProgress;
            }
        }

        public async Task RemoveMusic(Guid musicId)
        {
            var song = await _songRepository.GetAsync(SongId.FromGuid(musicId));
            if (song != null)
            {
                await _fileManager.DeleteFileAsync(song.SongPath);
                await _songRepository.RemoveAsync(song.Id);
                await _dispatcher.DispatchAsync(new SongRemoved(song.Id));
            }
        }

    }
}