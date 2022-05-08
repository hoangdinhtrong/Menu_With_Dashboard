using DesignDashboard.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;

namespace DesignDashboard.ViewModels
{
    public class MusicViewModel : INotifyPropertyChanged
    {
        private readonly CollectionViewSource MusicItemsCollection;

        public ICollectionView MusicSourceCollection => MusicItemsCollection.View;

        public MusicViewModel()
        {
            ObservableCollection<MusicItems> musicItems = new ObservableCollection<MusicItems>
            {

                new MusicItems { MusicName = "Bass", MusicImage = @"/Assets/note_icon.png" },
                new MusicItems { MusicName = "Beats", MusicImage = @"/Assets/note_icon.png" },
                new MusicItems { MusicName = "Electronic", MusicImage = @"/Assets/note_icon.png" },
                new MusicItems { MusicName = "Hip hop", MusicImage = @"/Assets/note_icon.png" },
                new MusicItems { MusicName = "Deep House", MusicImage = @"/Assets/note_icon.png" },
                new MusicItems { MusicName = "Instrumental", MusicImage = @"/Assets/note_icon.png" }

            };

            MusicItemsCollection = new CollectionViewSource { Source = musicItems };
            MusicItemsCollection.Filter += MenuItems_Filter;
        }

        //// Implement interface member for INotifyPropertyChanged.
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        //// Text Search Filter.
        private string? _filterText;
        public string? FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                MusicItemsCollection.View.Refresh();
                OnPropertyChanged();
            }
        }

        private void MenuItems_Filter(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrEmpty(FilterText))
            {
                e.Accepted = true;
                return;
            }

            DownloadItems? item = e.Item as DownloadItems;
            if (item != null && item.DownloadName.ToUpper().Contains(FilterText.ToUpper()))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }
    }
}
