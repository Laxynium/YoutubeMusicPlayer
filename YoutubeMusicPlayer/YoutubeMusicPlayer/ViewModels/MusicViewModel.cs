namespace YoutubeMusicPlayer.ViewModels
{
    public class MusicViewModel:ViewModelBase
    {
        private double _value;
        public double Value
        {
            get => _value;
            set => SetValue(ref _value, value);
        }
        public string VideoId { get; set; }

        public string Title { get; set; }

        private string _imageSource;
        public string ImageSource
        {
            get => _imageSource;
            set => SetValue(ref _imageSource,value);
        }

        public string FilePath { get; set; }
    }
}
