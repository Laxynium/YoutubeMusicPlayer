using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using YoutubeMusicPlayer.Annotations;

namespace YoutubeMusicPlayer.Framework
{
    public abstract class ViewModelBase:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetValue<T>(ref T backingProp, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingProp, newValue)) return;

            backingProp = newValue;

            OnPropertyChanged(propertyName);
        }
    }
}
