using DesignDashboard.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;

namespace DesignDashboard.ViewModels
{
    public class TrashViewModel : INotifyPropertyChanged
    {
        private readonly CollectionViewSource TrashItemsCollection;

        public ICollectionView TrashSourceCollection => TrashItemsCollection.View;

        public TrashViewModel()
        {
            ObservableCollection<TrashItems> trashItems = new ObservableCollection<TrashItems>
            {

                new TrashItems { TrashName = "Data.txt", TrashImage = @"/Assets/notepad_icon.png" }

            };

            TrashItemsCollection = new CollectionViewSource { Source = trashItems };
            TrashItemsCollection.Filter += MenuItems_Filter;
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
                TrashItemsCollection.View.Refresh();
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
