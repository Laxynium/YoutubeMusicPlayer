using Xamarin.Forms;

namespace YoutubeMusicPlayer.ViewModels
{
    public class MusicSearchViewModel
    {
        public Command OnMusicSearch;

        public Command OnSelectedItem;

        public Command OnTextChanged;

        public MusicSearchViewModel()
        {
            OnMusicSearch= new Command(SearchMusic);
            OnSelectedItem = new Command(SelectItem);
            OnTextChanged = new Command(ChangeText);
        }

        private void SearchMusic()
        {           

        }

        private void SelectItem()
        {          
        }

        private void ChangeText()
        {
            
        }
    }
}
