using System.Threading.Tasks;

namespace YoutubeMusicPlayer.MusicManagement.Domain
{
    public interface IUniqueMasterListRule
    {
        Task<bool> IsUnique();
    }
}