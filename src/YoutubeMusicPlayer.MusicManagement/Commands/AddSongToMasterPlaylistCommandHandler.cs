using System.Linq;
using System.Threading.Tasks;
using YoutubeMusicPlayer.Framework.Messaging;
using YoutubeMusicPlayer.MusicManagement.Domain;

namespace YoutubeMusicPlayer.MusicManagement.Commands
{
    public class AddSongToMasterPlaylistCommandHandler : ICommandHandler<AddSongToMasterPlaylistCommand>
    {
        private readonly PlaylistRepository _repository;
        private readonly IEventDispatcher _dispatcher;

        public AddSongToMasterPlaylistCommandHandler(PlaylistRepository repository, IEventDispatcher dispatcher)
        {
            _repository = repository;
            _dispatcher = dispatcher;
        }
        public async Task HandleAsync(AddSongToMasterPlaylistCommand command)
        {
            var masterPlaylist = await _repository.GetMasterAsync();

            masterPlaylist.AddSong(new SongId(command.SongId));

            await _dispatcher.DispatchAsync(masterPlaylist.Events.ToArray());
        }
    }
}