using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using YoutubeMusicPlayer.Domain.MusicSearching;
using YoutubeMusicPlayer.MusicSearching;
using YoutubeMusicPlayer.MusicSearching.ViewModels;

namespace Tests
{
    [TestFixture]
    public class MusicSearchViewModelTests
    {
        private Mock<IMusicSearchingService> _youtubeService;

        private MusicSearchViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            _youtubeService=new Mock<IMusicSearchingService>();

            _youtubeService.Setup(x => x.FindMusicAsync("Music")).ReturnsAsync(new List<MusicDto>
            {
                new MusicDto("1","titl1","imgs1"),
                new MusicDto("2","titl2","imgs2"),
                new MusicDto("3","titl3","imgs3"),
            });

            //_viewModel=new MusicSearchViewModel(_youtubeService.Object);
        }
        [Test]
        public void SearchMusic_WhenCalled_ShouldFillListView()
        {
            _viewModel.SearchText = "Music";
            _viewModel.MusicSearchCommand.Execute(null);
           
            _youtubeService.Verify(x=>x.FindMusicAsync("Music"));

            Assert.AreEqual(_viewModel.MusicListView.Count, 3);
        }

        [Test]
        public void SelectItem_WhenCalled_ShouldSelectedItemBeNull()
        {
            _viewModel.SearchText = "Music";
            _viewModel.MusicSearchCommand.Execute(null);

            Assert.IsNull(_viewModel.SelectedMusic);
        }

        [Test]
        public void ChangeText_WhenCalledWith0LengthStr_ShouldSetListViewToNull()
        {
            _viewModel.SearchText = "";
            _viewModel.TextChangeCommand.Execute(null);
            Assert.IsNull(_viewModel.MusicListView);
        }

    }
}
