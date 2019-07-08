using System.Threading.Tasks;

namespace YoutubeMusicPlayer.MusicDownloading
{
    public interface IScriptIdEncoder
    {
        Task<string> EncodeAsync(string scriptId);
    }
}