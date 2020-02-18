using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace YoutubeMusicPlayer.MusicManagement.Application.Services.Youtube
{
    public interface IYoutubeService
    {
        Task<Result<YoutubeSongDto>> DownloadSong(string ytId);
    }
}