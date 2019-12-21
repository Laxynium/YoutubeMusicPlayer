using YoutubeMusicPlayer.PlaylistManagement.ViewModels;

namespace YoutubeMusicPlayer.PlaylistManagement.Factories
{
    public interface IAddSongsViewModelFactor
    {
        AddSongsViewModel Create(PlaylistViewModel parent);
    }
}