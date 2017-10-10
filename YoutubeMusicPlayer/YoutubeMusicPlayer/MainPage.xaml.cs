using System;
using System.Diagnostics;
using System.Linq;
using Plugin.MediaManager.Abstractions.Implementations;
using Xamarin.Forms;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Models;
using YoutubeMusicPlayer.Repositories;
using YoutubeMusicPlayer.Services;
using YoutubeMusicPlayer.ViewModels;

namespace YoutubeMusicPlayer
{
    public partial class MainPage 
    {
        public MainPage(ContentPage[]pages)
        {
            InitializeComponent();
            Children.AddRange(pages);
            CurrentPage = Children.ToList().OfType<MusicSearchPage>().First();
        }
    }
}
