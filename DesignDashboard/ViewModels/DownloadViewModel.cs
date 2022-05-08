using DesignDashboard.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;

namespace DesignDashboard.ViewModels
{
    public class DownloadViewModel : INotifyPropertyChanged
    {
        private readonly CollectionViewSource DownloadItemsCollection;

        public ICollectionView DownloadSourceCollection => DownloadItemsCollection.View;

        public DownloadViewModel()
        {
            ObservableCollection<DownloadItems> downloadItems = new ObservableCollection<DownloadItems>
            {
                new DownloadItems { DownloadName = "Visual Studio 2019", DownloadImage = @"/Assets/vs_icon.png" },
                new DownloadItems { DownloadName = "Android Studio", DownloadImage = @"/Assets/android_icon.png" },
                new DownloadItems { DownloadName = "Python", DownloadImage = @"/Assets/python_icon.png" },
                new DownloadItems { DownloadName = "Swift", DownloadImage = @"/Assets/swift_icon.png" },
                new DownloadItems { DownloadName = "Visual Studio Code", DownloadImage = @"/Assets/vsc_icon.png" },
                new DownloadItems { DownloadName = "Javascript", DownloadImage = @"/Assets/javascript_icon.png" },
                new DownloadItems { DownloadName = "HTML 5", DownloadImage = @"/Assets/html_icon.png" },
                new DownloadItems { DownloadName = "Angular", DownloadImage = @"/Assets/angular_icon.png" },
                new DownloadItems { DownloadName = "Flutter", DownloadImage = @"/Assets/flutter_icon.png" }
            };

            DownloadItemsCollection = new CollectionViewSource { Source = downloadItems };
            DownloadItemsCollection.Filter += MenuItems_Filter;
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
                DownloadItemsCollection.View.Refresh();
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
