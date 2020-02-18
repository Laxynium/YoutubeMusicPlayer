using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YoutubeMusicPlayer.Framework.Messaging;
using YoutubeMusicPlayer.MusicManagement.Domain;
using YoutubeMusicPlayer.MusicManagement.Domain.ValueObjects;

namespace YoutubeMusicPlayer.MusicManagement.Application.Commands.Handlers
{
    public class RemoveSongHandler : ICommandHandler<RemoveSong>
    {
        private readonly IMainPlaylistRepository _mainPlaylistRepository;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly MusicManagementOptions _options;

        public RemoveSongHandler(IMainPlaylistRepository mainPlaylistRepository, IEventDispatcher eventDispatcher, MusicManagementOptions options)
        {
            _mainPlaylistRepository = mainPlaylistRepository;
            _eventDispatcher = eventDispatcher;
            _options = options;
        }
        public async Task HandleAsync(RemoveSong command)
        {
            var mainPlaylist = await _mainPlaylistRepository.GetAsync();

            var removedSong = mainPlaylist.RemoveSong(new SongId(command.SongId));

            File.Delete(Path.Combine(_options.MusicDirectory, removedSong.SongPath));

            await _mainPlaylistRepository.UpdateAsync(mainPlaylist);
            await _eventDispatcher.DispatchAsync(mainPlaylist.Events.ToArray());
        }
    }
}