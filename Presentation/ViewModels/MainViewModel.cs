using Bookshelf.Presentation.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bookshelf.Presentation.ViewModels
{
    internal class MainViewModel : ViewModelBase {
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel {
            get => _currentViewModel;
            set {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public ICommand ShowBooksCommand { get; }
        public ICommand ShowGenresCommand { get; }
        public ICommand ShowAuthorsCommand { get; }

        public MainViewModel() {
            //ShowBooksCommand = new RelayCommand(o => CurrentViewModel = new BooksViewModel());
            //ShowGenresCommand = new RelayCommand(o => CurrentViewModel = new GenresViewModel());
            //ShowAuthorsCommand = new RelayCommand(o => CurrentViewModel = new AuthorsViewModel());

            // Default view
            //CurrentViewModel = new BooksViewModel();
        }
    }
}
