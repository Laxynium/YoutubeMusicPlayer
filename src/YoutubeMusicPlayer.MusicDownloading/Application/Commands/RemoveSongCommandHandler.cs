using System.IO;
using System.Threading.Tasks;
using ChinhDo.Transactions.FileManager;
using YoutubeMusicPlayer.Framework;
using YoutubeMusicPlayer.Framework.Messaging;
using YoutubeMusicPlayer.MusicDownloading.Application.Events;
using YoutubeMusicPlayer.MusicDownloading.Application.Repositories;

namespace YoutubeMusicPlayer.MusicDownloading.Application.Commands
{
    public class RemoveSongCommandHandler : ICommandHandler<RemoveSongCommand>
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly ISongRepository _songRepository;
        private readonly FileManager _fileManager;

        public RemoveSongCommandHandler(
            IEventDispatcher eventDispatcher, 
            ISongRepository songRepository,
            FileManager fileManager)
        {
            _eventDispatcher = eventDispatcher;
            _songRepository = songRepository;
            _fileManager = fileManager;
        }
        public async Task HandleAsync(RemoveSongCommand command)
        {
            var song = await _songRepository.GetAsync(command.SongIdGuid);
            if(song is null)
                return;

            if(File.Exists(song.FilePath)) // user could delete song manually from FS
                await _fileManager.DeleteAsync(song.FilePath);
            await _songRepository.RemoveAsync(song);

            await _eventDispatcher.DispatchAsync(new SongRemoved(song.Id));
        }
    }
}