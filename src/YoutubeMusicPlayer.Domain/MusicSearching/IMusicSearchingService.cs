using System.Collections.Generic;
using System.Threading.Tasks;

namespace YoutubeMusicPlayer.Domain.MusicSearching
{
    public interface IMusicSearchingService
    {
        Task<IEnumerable<MusicDto>> FindMusicAsync(string title);
    }
}
