using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace YoutubeMusicPlayer.Domain.MusicDownloading
{
    public interface ISongService
    {
        event EventHandler<(string ytId, string title, string imageSource)> OnDownloadStart;
        event EventHandler<MusicDto> OnDownloadFinished;
        event EventHandler<(string ytId, string errorMessage)> OnDownloadFailed;
        event EventHandler<(string ytId, double progress)> OnDownloadProgress;
        Task DownloadAndSaveMusic(string youtubeId, string title, string imageSource);
    }

    public class MusicDto
    {
        public Guid Id { get; }
        public string YoutubeId { get; }
        public string Title { get; }
        public string ImageSource { get; }
        public string FilePath { get; }

        public MusicDto(Guid id, string youtubeId, string title, string imageSource, string filePath)
        {
            Id = id;
            YoutubeId = youtubeId;
            Title = title;
            ImageSource = imageSource;
            FilePath = filePath;
        }
    }
}