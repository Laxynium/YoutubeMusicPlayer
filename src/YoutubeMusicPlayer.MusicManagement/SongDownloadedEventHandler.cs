using System.Threading.Tasks;
using YoutubeMusicPlayer.Framework.Messaging;
using YoutubeMusicPlayer.MusicDownloading.Application.Events;
using YoutubeMusicPlayer.MusicManagement.Commands;

namespace YoutubeMusicPlayer.MusicManagement
{
    public class SongDownloadedEventHandler : IEventHandler<SongDownloaded>
    {
        private readonly ICommandDispatcher _dispatcher;

        public SongDownloadedEventHandler(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }
        public async Task HandleAsync(SongDownloaded @event)
        {
            await _dispatcher.DispatchAsync(new AddSongToMasterPlaylistCommand(@event.Id));
        }
    }
}
