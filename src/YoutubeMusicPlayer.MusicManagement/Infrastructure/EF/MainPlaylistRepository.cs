using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YoutubeMusicPlayer.MusicManagement.Domain;
using YoutubeMusicPlayer.MusicManagement.Domain.Entities;

namespace YoutubeMusicPlayer.MusicManagement.Infrastructure.EF
{
    internal sealed class MainPlaylistRepository : IMainPlaylistRepository
    {
        private readonly MusicManagementDbContext _context;

        public MainPlaylistRepository(MusicManagementDbContext context)
        {
            _context = context;
        }

        public async Task<MainPlaylist> GetAsync()
        {
            return await _context.MainPlaylist.FirstAsync();
        }

        public async Task UpdateAsync(MainPlaylist mainPlaylist)
        {
            _context.MainPlaylist.Update(mainPlaylist);
            await _context.SaveChangesAsync();
        }
    }
}