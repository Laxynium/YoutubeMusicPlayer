using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YoutubeMusicPlayer.Framework.Messaging;
using YoutubeMusicPlayer.MusicDownloading.Infrastructure;

namespace YoutubeMusicPlayer.MusicDownloading.ReadModel
{
    public class GetAllDownloadedSongsQueryHandler : IQueryHandler<GetAllDownloadedSongsQuery,IList<SongDto>>
    {
        private readonly DbContextOptions _options;

        public GetAllDownloadedSongsQueryHandler(DbContextOptions options)
        {
            _options = options;
        }
        public async Task<IList<SongDto>> HandleAsync(GetAllDownloadedSongsQuery query)
        {
            using var context = new MusicDownloadingDbContext(_options);

            return await context.Songs.AsNoTracking().Select(x=>new SongDto(x.Id,x.YtId,x.Title,x.ImageSource,1)).ToListAsync();
        }
    }
}