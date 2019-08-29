using System.Threading.Tasks;
using YoutubeMusicPlayer.Domain.Framework;
using YoutubeMusicPlayer.Domain.MusicDownloading.Events;
using YoutubeMusicPlayer.Domain.MusicManagement.Commands;

namespace YoutubeMusicPlayer.Domain.MusicManagement.EventHandlers
{
    public class SongCreatedEventHandler : IEventHandler<SongCreated>
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public SongCreatedEventHandler(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }
        public async Task HandleAsync(SongCreated @event)
        {
            await _commandDispatcher.DispatchAsync(new AddSongToAllSongsPlaylist(@event.Id));
        }
    }
}