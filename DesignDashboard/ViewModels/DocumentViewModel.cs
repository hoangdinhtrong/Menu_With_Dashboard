using DesignDashboard.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;

namespace DesignDashboard.ViewModels
{
    public class DocumentViewModel : INotifyPropertyChanged
    {
        private readonly CollectionViewSource DocumentItemsCollection;

        public DocumentViewModel()
        {
            ObservableCollection<DocumentItems> documentItems = new ObservableCollection<DocumentItems>
            {

                new DocumentItems { DocumentName = "Books", DocumentImage = @"/Assets/book_icon.png" },
                new DocumentItems { DocumentName = "Studio", DocumentImage = @"/Assets/studio_icon.png" },
                new DocumentItems { DocumentName = "Export", DocumentImage = @"/Assets/export_icon.png" },
                new DocumentItems { DocumentName = "Print", DocumentImage = @"/Assets/print_icon.png" },
                new DocumentItems { DocumentName = "Orders", DocumentImage = @"/Assets/order_icon.png" },
                new DocumentItems { DocumentName = "Stocks", DocumentImage = @"/Assets/stock_icon.png" },
                new DocumentItems { DocumentName = "Invoice", DocumentImage = @"/Assets/invoice_icon.png" }

            };

            DocumentItemsCollection = new CollectionViewSource { Source = documentItems };
            DocumentItemsCollection.Filter += MenuItems_Filter;
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
                DocumentItemsCollection.View.Refresh();
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

            DocumentItems? item = e.Item as DocumentItems;
            if (item != null && item.DocumentName.ToUpper().Contains(FilterText.ToUpper()))
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
