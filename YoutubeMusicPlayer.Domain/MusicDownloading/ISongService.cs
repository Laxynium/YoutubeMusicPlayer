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
        Task RemoveMusic(Guid musicId);
    }
}