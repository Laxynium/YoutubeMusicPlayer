using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Domain.MusicDownloading;
using YoutubeMusicPlayer.Domain.MusicDownloading.Repositories;

namespace YoutubeMusicPlayer.MusicDownloading
{
    public class SongService : ISongService
    {
        private readonly ISongDownloader _songDownloader;
        private readonly ISongRepository _songRepository;
        private readonly IFileManager _fileManager;

        public SongService(ISongDownloader songDownloader, ISongRepository songRepository, IFileManager fileManager)
        {
            _songRepository = songRepository;
            _fileManager = fileManager;
            _songDownloader = songDownloader;
        }

        public event EventHandler<(string ytId, string title, string imageSource)> OnDownloadStart;
        public event EventHandler<MusicDto> OnDownloadFinished;
        public event EventHandler<(string ytId, string errorMessage)> OnDownloadFailed;
        public event EventHandler<(string ytId,double progress)> OnDownloadProgress;

        public async Task DownloadAndSaveMusic(string youtubeId, string title, string imageSource)
        {
            if (await _songRepository.Exists(youtubeId))
                return;//TODO think what shold be displayed to user when he wants to download again same music

            OnDownloadStart?.Invoke(this, (youtubeId,title,imageSource));
            _songDownloader.OnDownloadProgress += OnDownloadProgress;
            try
            {
                var path = await _songDownloader.DownloadMusic(youtubeId, title);

                var song = new Song(youtubeId, title, imageSource, path);

                await _songRepository.AddAsync(song);

                OnDownloadFinished?.Invoke(this, new MusicDto(song.Id,song.YoutubeId,song.Title,song.ImageSource,song.FilePath));
            }
            catch (Exception e)
            {
                OnDownloadFailed?.Invoke(this, (youtubeId, e.Message));
            }
            finally
            {
                _songDownloader.OnDownloadProgress -= OnDownloadProgress;
            }
        }

        public async Task RemoveMusic(Guid musicId)
        {
            var song = await _songRepository.GetAsync(musicId);
            if (song != null)
            {
                await _fileManager.DeleteFileAsync(song.FilePath);
                await _songRepository.RemoveAsync(musicId);
            }
        }
    }
}