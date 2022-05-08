/// <summary>
/// ViewModel - ["The Connector"]
/// ViewModel exposes data contained in the Model object to the View.
/// The ViewModel performs all modifications made to the Model data
/// </summary>

using DesignDashboard.Commands;
using DesignDashboard.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Windows.Input;

namespace DesignDashboard.ViewModels
{
    public class NavigationViewModel : INotifyPropertyChanged
    {
        //// CollectionViewSource enables XAML code to set the commonly used CollectionView properties,
        //// passing these settings to the underlying view.
        private CollectionViewSource MenuItemsCollection;

        public ICollectionView SourceCollection => MenuItemsCollection.View;

        public NavigationViewModel()
        {
            ObservableCollection<MenuItems> menuItems = new ObservableCollection<MenuItems>
            {
                new MenuItems { MenuName = "Home", MenuImage = @"/Assets/Home_Icon.png" },
                new MenuItems { MenuName = "Desktop", MenuImage = @"/Assets/Desktop_Icon.png" },
                new MenuItems { MenuName = "Documents", MenuImage = @"/Assets/Document_Icon.png" },
                new MenuItems { MenuName = "Downloads", MenuImage = @"/Assets/Download_Icon.png" },
                new MenuItems { MenuName = "Pictures", MenuImage = @"/Assets/Images_Icon.png" },
                new MenuItems { MenuName = "Music", MenuImage = @"/Assets/Music_Icon.png" },
                new MenuItems { MenuName = "Movies", MenuImage = @"/Assets/Movies_Icon.png" },
                new MenuItems { MenuName = "Trash", MenuImage = @"/Assets/Trash_Icon.png" }
            };

            MenuItemsCollection = new CollectionViewSource { Source = menuItems };
            MenuItemsCollection.Filter += MenuItems_Filter;

            //// Set Startup Page
            SelectedViewModel = new StartupViewModel();
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
                MenuItemsCollection.View.Refresh();
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

            MenuItems? item = e.Item as MenuItems;
            if (item != null && item.MenuName.ToUpper().Contains(FilterText.ToUpper()))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        //// Select ViewModel
        private object? _selectedViewModel;
        public object? SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Switch View Model
        /// </summary>
        /// <param name="parameter"></param>
        public void SwitchViews(object? parameter)
        {
            if(parameter == null)
            {
                SelectedViewModel = new HomeViewModel();
            }

            switch (parameter)
            {
                case "Home":
                    SelectedViewModel = new HomeViewModel();
                    break;
                case "Desktop":
                    SelectedViewModel = new DesktopViewModel();
                    break;
                case "Documents":
                    SelectedViewModel = new DocumentViewModel();
                    break;
                case "Downloads":
                    SelectedViewModel = new DownloadViewModel();
                    break;
                case "Pictures":
                    SelectedViewModel = new PictureViewModel();
                    break;
                case "Music":
                    SelectedViewModel = new MusicViewModel();
                    break;
                case "Movies":
                    SelectedViewModel = new MovieViewModel();
                    break;
                case "Trash":
                    SelectedViewModel = new TrashViewModel();
                    break;
                default:
                    SelectedViewModel = new HomeViewModel();
                    break;
            }
        }

        //// Menu Button Command
        private ICommand? _menuCommand;
        public ICommand? MenuCommand
        {
            get
            {
                if(_menuCommand == null)
                {
                    _menuCommand = new RelayCommand(param => SwitchViews(param));
                }
                return _menuCommand;
            }
        }

        #region "PC View"
        //// Show PC View 
        public void PCView()
        {
            SelectedViewModel = new PCViewModel();
        }

        //// This PC Button Command
        private ICommand? _pcCommand;
        public ICommand? PCCommand
        {
            get
            {
                if(_pcCommand == null)
                {
                    _pcCommand = new RelayCommand(param => PCView());
                }
                return _pcCommand;
            }
        }
        #endregion

        #region "Home View"
        //// Show Home View 
        private void ShowHome()
        {
            SelectedViewModel = new HomeViewModel();
        }

        //// Back Button Command
        private ICommand? _backHomeCommand;
        public ICommand? BackHomeCommand
        {
            get
            {
                if(_backHomeCommand == null)
                {
                    _backHomeCommand = new RelayCommand(param => ShowHome());
                }
                return _backHomeCommand;
            }
        }
        #endregion

        #region "Close App"
        //// Close Application
        public void CloseApp(object? obj)
        {
            if (obj == null)
                return;

            MainWindow? win = obj as MainWindow;
            win?.Close();
        }

        //// Close App Command
        private ICommand? _closeCommand;
        public ICommand? CloseCommand
        {
            get
            {
                if(_closeCommand == null)
                {
                    _closeCommand = new RelayCommand(param => CloseApp(param));
                }
                return _closeCommand;
            }
        }
        #endregion
    }
}
