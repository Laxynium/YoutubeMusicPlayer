using System.Threading.Tasks;
using YoutubeMusicPlayer.Domain.Framework;
using YoutubeMusicPlayer.Domain.MusicDownloading.Events;
using YoutubeMusicPlayer.Domain.MusicManagement.Commands;

namespace YoutubeMusicPlayer.Domain.MusicManagement.EventHandlers
{
    public class SongRemovedEventHandler : IEventHandler<SongRemoved>
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public SongRemovedEventHandler(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }
        public async Task HandleAsync(SongRemoved @event)
        {
            await _commandDispatcher.DispatchAsync(new RemoveSongFromAllSongsPlaylist(@event.SongId));
        }
    }
}