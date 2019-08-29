using System.Threading.Tasks;
using YoutubeMusicPlayer.Domain.Framework;
using YoutubeMusicPlayer.Domain.SharedKernel;

namespace YoutubeMusicPlayer.Domain.MusicManagement.Commands
{
    public class AddSongToAllSongsPlaylistHandler : ICommandHandler<AddSongToAllSongsPlaylist>
    {
        private readonly IPlaylistRepository _playlistRepository;

        public AddSongToAllSongsPlaylistHandler(IPlaylistRepository playlistRepository)
        {
            _playlistRepository = playlistRepository;
        }
        public async Task HandleAsync(AddSongToAllSongsPlaylist command)
        {
            var allSongsPlaylist = await _playlistRepository.GetAsync();

            var song = new Song(SongId.FromGuid(command.Id));
            allSongsPlaylist.AddSong(song);

            await _playlistRepository.SaveAsync(allSongsPlaylist);
        }
    }
}