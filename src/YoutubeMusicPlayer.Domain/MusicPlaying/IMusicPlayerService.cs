using System;
using System.Threading.Tasks;

namespace YoutubeMusicPlayer.Domain.MusicPlaying
{
    public interface IMusicPlayerService
    {
        Task TogglePlay();
        Task ChangeSong(Guid songId);
        Task GoToNext();
        Task GoToPrevious();
        Task ChangePlaylist(Guid playlistId);
        Task ChangeSongPosition(Guid songId, double timestamp);
        event EventHandler<string> OnMusicChanged;
        event EventHandler<string> OnProgressChanged;
    }
}