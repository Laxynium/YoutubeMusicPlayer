using System.Threading.Tasks;

namespace YoutubeMusicPlayer.AbstractLayer
{
    public interface IScriptIdEncoder
    {
        Task<string> EncodeAsync(string scriptId);
    }
}