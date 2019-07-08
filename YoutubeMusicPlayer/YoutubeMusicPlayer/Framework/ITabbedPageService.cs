using System.Threading.Tasks;

namespace YoutubeMusicPlayer.Framework
{
    public interface ITabbedPageService
    {
        Task ChangePage(int index);
    }
}
