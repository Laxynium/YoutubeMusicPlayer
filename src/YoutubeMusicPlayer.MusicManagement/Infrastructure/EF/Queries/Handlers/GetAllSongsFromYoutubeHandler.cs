using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YoutubeMusicPlayer.Framework.Messaging;
using YoutubeMusicPlayer.MusicManagement.Application.Queries;

namespace YoutubeMusicPlayer.MusicManagement.Infrastructure.EF.Queries.Handlers
{
    public class GetAllSongsFromYoutubeHandler : IQueryHandler<GetAllSongsFromYoutube,IEnumerable<SongDto>>
    {
        private readonly DbContextOptions _options;

        public GetAllSongsFromYoutubeHandler(DbContextOptions options)
        {
            _options = options;
        }
        public async Task<IEnumerable<SongDto>> HandleAsync(GetAllSongsFromYoutube query)
        {
            using var context = new MusicManagementDbContext(_options);

            var songDtos = await context.MainPlaylist.AsNoTracking()
                .SelectMany(x => x.Songs)
                .Select(x =>new SongDto(x.Id.Value, x.YtId, x.Title, x.ThumbnailUrl))
                .ToListAsync();
            return songDtos;
        }
    }
}