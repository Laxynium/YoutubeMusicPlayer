using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace YoutubeMusicPlayer.Domain.MusicDownloading
{
    public interface ISongService
    {
        Task DownloadAndCreateSong(string youtubeId, string title, string imageSource);
        Task RemoveMusic(Guid musicId);
    }
}