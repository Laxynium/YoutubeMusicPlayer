using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YoutubeMusicPlayer.MusicManagement.Domain;

namespace YoutubeMusicPlayer.MusicManagement
{
    public class PlaylistRepository
    {
        private readonly MusicManagementDbContext _context;

        public PlaylistRepository(MusicManagementDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Playlist playlist)
        {
            await _context.Playlists.AddAsync(playlist);
        }

        public async Task<Playlist> GetMasterAsync()
        {
            return await _context.Playlists.SingleAsync(x => x.IsMaster);
        }
    }
}