using Bookshelf.Domain.DataModels;
using Bookshelf.Presentation.Base;
using Bookshelf.Presentation.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Bookshelf.Presentation.ViewModels
{
    internal class MainViewModel : ViewModelBase {
        private MainModel mainModel;

        public ObservableCollection<string> Authors => mainModel.GetAuthorsNames();
        public ObservableCollection<string> Genres => mainModel.GetGenresNames();


        private Visibility _isMenuOpen;

        public Visibility IsMenuOpen {
            get => _isMenuOpen;
            set {
                _isMenuOpen = value;
                OnPropertyChanged(nameof(IsMenuOpen));
            }
        }

        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel {
            get => _currentViewModel;
            set {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public ICommand OpenMenuCommand {  get; }


        public MainViewModel(MainModel model) {
            mainModel = model;
            IsMenuOpen = Visibility.Collapsed;

            OpenMenuCommand = new RelayCommand(SwitchMenuState);
        }

        private void SwitchMenuState() {
            if (IsMenuOpen == Visibility.Collapsed) {
                IsMenuOpen = Visibility.Visible;
            } else {
                IsMenuOpen = Visibility.Collapsed;
            }
        }
    }
}
