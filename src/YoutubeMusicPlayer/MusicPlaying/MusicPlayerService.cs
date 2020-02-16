 using System;
using System.Threading.Tasks;

namespace YoutubeMusicPlayer.MusicPlaying
{
    public class MusicPlayerService 
    {
        public MusicPlayerService()
        {
            
        }
        public async Task TogglePlay()
        {

        }

        public async Task ChangeSong(Guid songId)
        {
        }

        public async Task GoToNext()
        {
            
        }

        public async Task GoToPrevious()
        {
            
        }

        public async Task ChangePlaylist(Guid playlistId)
        {
            
        }

        public async Task ChangeSongPosition(Guid songId, double timestamp)
        {
            
        }

        public event EventHandler<string> OnMusicChanged;
        public event EventHandler<string> OnProgressChanged;
    }
}