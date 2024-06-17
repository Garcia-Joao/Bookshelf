using Bookshelf.Domain.DataModels;
using Bookshelf.Presentation.Base;
using Bookshelf.Presentation.Models;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly MainModel _mainModel;
        private readonly IServiceProvider _serviceProvider;

        public ObservableCollection<string> Authors => _mainModel.GetAuthorsNames();
        public ObservableCollection<string> Genres => _mainModel.GetGenresNames();

        private string _authorFilter;
        public string AuthorFilter {
            get => _authorFilter;
            set {
                _authorFilter = value;
                OnPropertyChanged(nameof(AuthorFilter));
                ApplyBookFilters();
            }
        }

        private string _genreFilter;
        public string GenreFilter {
            get => _genreFilter;
            set {
                _genreFilter = value;
                OnPropertyChanged(nameof(GenreFilter));
                ApplyBookFilters();
            }
        }

        private string _bookNameFilter;
        public string BookNameFilter {
            get => _bookNameFilter;
            set {
                _bookNameFilter = value;
                OnPropertyChanged(nameof(BookNameFilter));
                ApplyBookFilters();
            }
        }

        private string _currentViewName;
        public string CurrentViewName {
            get => _currentViewName;
            set {
                _currentViewName = value;
                OnPropertyChanged(nameof(CurrentViewName));
            }
        }

        private string _firstMenuText;
        public string FirstMenuText {
            get => _firstMenuText;
            set {
                _firstMenuText = value;
                OnPropertyChanged(nameof(FirstMenuText));
            }
        }

        private string _secondMenuText;
        public string SecondMenuText {
            get => _secondMenuText;
            set {
                _secondMenuText = value;
                OnPropertyChanged(nameof(SecondMenuText));
            }
        }

        private Visibility _overlayVisibility;

        public Visibility OverlayVisibility {
            get => _overlayVisibility;
            set {
                _overlayVisibility = value;
                OnPropertyChanged(nameof(OverlayVisibility));
            }
        }

        private Visibility _isMenuOpen;
        public Visibility IsMenuOpen {
            get => _isMenuOpen;
            set {
                _isMenuOpen = value;
                OnPropertyChanged(nameof(IsMenuOpen));
            }
        }
        private ViewModelBase _currentOverlay;
        private ViewModelBase _currentViewModel;


        public ICommand OpenMenuCommand { get; }


        private ICommand _addCommand;
        private ICommand _firstMenuCommand;
        private ICommand _secondMenuCommand;

        public ICommand AddCommand { 
            get => _addCommand;
            set {
                _addCommand = value;
                OnPropertyChanged(nameof(AddCommand));
            }
        }

        public ICommand FirstMenuCommand { 
            get => _firstMenuCommand;
            set {
                _firstMenuCommand = value;
                OnPropertyChanged(nameof(FirstMenuCommand));
            }
        }

        public ICommand SecondMenuCommand {
            get => _secondMenuCommand;
            set {
                _secondMenuCommand = value;
                OnPropertyChanged(nameof(SecondMenuCommand));
            }
        }

        public ViewModelBase CurrentOverlay {
            get => _currentOverlay;
            set {
                _currentOverlay = value;
                OnPropertyChanged(nameof(CurrentOverlay));
            }
        }

        public ViewModelBase CurrentViewModel {
            get => _currentViewModel;
            set {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public MainViewModel(MainModel model, IServiceProvider serviceProvider) : base(serviceProvider) {
            _mainModel = model;
            _serviceProvider = serviceProvider;

            OpenMenuCommand = new RelayCommand(SwitchMenuState);
            AddCommand = new RelayCommand(SwitchAddState);

            SetBooksState();

            IsMenuOpen = Visibility.Collapsed;
            OverlayVisibility = Visibility.Collapsed;
        }

        private void SetBooksState() {
            CurrentViewModel = _serviceProvider.GetRequiredService<BooksViewModel>();
            (CurrentViewModel as BooksViewModel).EditCardCallback += EditBook;

            CurrentViewName = "Books";

            FirstMenuCommand = new RelayCommand(SetGenreState);
            FirstMenuText = "Genres";
            SecondMenuCommand = new RelayCommand(SetAuthorState);
            SecondMenuText = "Authors";
        }

        private void SetAuthorState() {
            throw new NotImplementedException();
        }

        private void SetGenreState() {
            throw new NotImplementedException();
        }

        private void EditBook(Guid guid) {
            OverlayVisibility = Visibility.Visible;
            var book = _mainModel.GetBookById(guid);
            var overlayVM = new BookOverlayViewModel(serviceProvider, book.Id, book.title, book.description, book.author, book.genre ,Authors, Genres);
            overlayVM.CancelAction += CloseOverlay;
            overlayVM.CreateAction += UpdateBook;
            CurrentOverlay = overlayVM;
        }

        private void SwitchAddState() {
            if (CurrentViewModel is BooksViewModel) {
                OverlayVisibility = Visibility.Visible;
                var overlayVM = new BookOverlayViewModel(serviceProvider, Authors, Genres);
                overlayVM.CancelAction += CloseOverlay;
                overlayVM.CreateAction += CreateNewBook;
                CurrentOverlay = overlayVM;
            }
        }

        private void CloseOverlay() {
            OverlayVisibility = Visibility.Collapsed;
            CurrentOverlay = null;
        }

        private void CreateNewBook(Book book) {
            serviceProvider.GetRequiredService<BooksModel>().AddBook(book);
            CloseOverlay();
            (CurrentViewModel as BooksViewModel).GeneralUpdate();
        }

        private void UpdateBook(Book book) {
            serviceProvider.GetRequiredService<BooksModel>().UpdateBook(book);
            CloseOverlay();
            (CurrentViewModel as BooksViewModel).GeneralUpdate();
        }

        private void ApplyBookFilters() {
            if (CurrentViewModel is BooksViewModel bookVm) {
                bookVm.UpdateFilters(AuthorFilter, GenreFilter, BookNameFilter);
            }
        }

        private void SwitchMenuState() {
            IsMenuOpen = IsMenuOpen == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
