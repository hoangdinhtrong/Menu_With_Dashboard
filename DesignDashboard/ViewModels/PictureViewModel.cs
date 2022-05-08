using DesignDashboard.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;

namespace DesignDashboard.ViewModels
{
    public class PictureViewModel : INotifyPropertyChanged
    {
        private readonly CollectionViewSource PictureItemsCollection;

        public ICollectionView PictureSourceCollection => PictureItemsCollection.View;

        public PictureViewModel()
        {
            ObservableCollection<PictureItems> pictureItems = new ObservableCollection<PictureItems>
            {

                new PictureItems { PictureName = "Logo", PictureImage = @"/Assets/channel_icon.png" }

            };

            PictureItemsCollection = new CollectionViewSource { Source = pictureItems };
            PictureItemsCollection.Filter += MenuItems_Filter;
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
                PictureItemsCollection.View.Refresh();
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
