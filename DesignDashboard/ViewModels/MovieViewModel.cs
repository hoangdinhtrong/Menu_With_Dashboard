using DesignDashboard.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;

namespace DesignDashboard.ViewModels
{
    public class MovieViewModel : INotifyPropertyChanged
    {
        private readonly CollectionViewSource MovieItemsCollection;

        public ICollectionView MovieSourceCollection => MovieItemsCollection.View;

        public MovieViewModel()
        {
            ObservableCollection<MovieItems> movieItems = new ObservableCollection<MovieItems>
            {

                new MovieItems { MovieName = "Action", MovieImage = @"/Assets/clap_icon.png" },
                new MovieItems { MovieName = "Thriller", MovieImage = @"/Assets/clap_icon.png" },
                new MovieItems { MovieName = "Adventure", MovieImage = @"/Assets/clap_icon.png" },
                new MovieItems { MovieName = "Drama", MovieImage = @"/Assets/clap_icon.png" },
                new MovieItems { MovieName = "Fantasy", MovieImage = @"/Assets/clap_icon.png" },
                new MovieItems { MovieName = "Mystery", MovieImage = @"/Assets/clap_icon.png" }

            };

            MovieItemsCollection = new CollectionViewSource { Source = movieItems };
            MovieItemsCollection.Filter += MenuItems_Filter;
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
                MovieItemsCollection.View.Refresh();
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
