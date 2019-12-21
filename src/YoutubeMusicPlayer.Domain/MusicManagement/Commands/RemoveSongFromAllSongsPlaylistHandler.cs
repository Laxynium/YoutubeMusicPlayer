using System.Threading.Tasks;
using YoutubeMusicPlayer.Domain.Framework;
using YoutubeMusicPlayer.Domain.SharedKernel;

namespace YoutubeMusicPlayer.Domain.MusicManagement.Commands
{
    public class RemoveSongFromAllSongsPlaylistHandler : ICommandHandler<RemoveSongFromAllSongsPlaylist>
    {
        private readonly IPlaylistRepository _playlistRepository;

        public RemoveSongFromAllSongsPlaylistHandler(IPlaylistRepository playlistRepository)
        {
            _playlistRepository = playlistRepository;
        }
        public async Task HandleAsync(RemoveSongFromAllSongsPlaylist command)
        {
            var allSongsPlaylist = await _playlistRepository.GetAsync();

            allSongsPlaylist.RemoveSong(SongId.FromGuid(command.Id));

            await _playlistRepository.SaveAsync(allSongsPlaylist);
        }
    }
}