using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using YoutubeMusicPlayer.Models;
using YoutubeMusicPlayer.Services;
using YoutubeMusicPlayer.ViewModels;

namespace Tests
{
    [TestFixture]
    public class MusicSearchViewModelTests
    {
        private Mock<IYoutubeService> _youtubeService;

        private Mock<IDownloadPageService> _pageService;

        private MusicSearchViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            _youtubeService=new Mock<IYoutubeService>();

            _youtubeService.Setup(x => x.FindMusicAsync("Music")).ReturnsAsync(new List<Music>
            {
                new Music
                {
                    ImageSource = "imgs1",
                    Title = "titl1",
                    VideoId = "1"
                },
                new Music
                {
                    ImageSource = "imgs2",
                    Title = "titl2",
                    VideoId = "2"
                },
                new Music
                {
                    ImageSource = "imgs3",
                    Title = "titl3",
                    VideoId = "3"
                },

            });

            _pageService =new Mock<IDownloadPageService>();
            _viewModel=new MusicSearchViewModel(_youtubeService.Object,_pageService.Object);
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
