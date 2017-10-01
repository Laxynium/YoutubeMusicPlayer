using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace YoutubeMusicPlayer.Models
{
    public class Music: INotifyPropertyChanged, INotifyProgressChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<int> ProgressChanged;

        private double _value;
        public double Value
        {
            get => _value;
            set
            {
                if (Math.Abs(_value - value) < 0.01) return;

                _value = value;

                OnPropertyChanged();
            }
        }
        [PrimaryKey]
        public string VideoId { get; set; }

        public string Title { get; set; }


        private string _imageSource;

        public string ImageSource
        {
            get=>_imageSource;          
            set
            {
                if (value == _imageSource) return;

                _imageSource = value;

                OnPropertyChanged();
            }
        }

        public string FilePath { get; set; }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnProgressChanged(int value)
        {
            ProgressChanged?.Invoke(this, value);
        }
    }
}
