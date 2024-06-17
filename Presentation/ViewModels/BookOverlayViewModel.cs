using Bookshelf.Domain.DataModels;
using Bookshelf.Domain.Services;
using Bookshelf.Presentation.Base;
using Bookshelf.Presentation.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bookshelf.Presentation.ViewModels {
    public class BookOverlayViewModel : ViewModelBase {

        Guid bookId { get; init; }

        private string _bookTitle;
        private string _bookDescription;
        private string _authorName;
        private string _genreName;

        private Author _author {
            set {
                AuthorName = value.Name;
            }
        }

        private Genre _genre {
            set {
                GenreName = value.Name;
            }
        }

        public string BookTitle {
            get => _bookTitle;
            set {
                _bookTitle = value;
                OnPropertyChanged(BookTitle);
            }
        }
        public string BookDescription {
            get => _bookDescription;
            set {
                _bookDescription = value;
                OnPropertyChanged(BookDescription);
            }
        }
        public string AuthorName {
            get => _authorName;
            set {
                _authorName = value;
                OnPropertyChanged(AuthorName);
            }
        }
        public string GenreName {
            get => _genreName;
            set {
                _genreName = value;
                OnPropertyChanged(GenreName);
            }
        }

        public ObservableCollection<string> Authors { get; }
        public ObservableCollection<string> Genres { get; }

        public Action CancelAction;
        public Action<Book> CreateAction;
        public ICommand CancelCommand { get; }
        public ICommand SaveCommand { get; }

        public BookOverlayViewModel(IServiceProvider serviceProvider, Guid bookId, string bookTitle, string bookDescription,Author author, Genre genre, ObservableCollection<string> authors, ObservableCollection<string> genres) : base(serviceProvider) {
            this.bookId = bookId;
            BookTitle = bookTitle;
            BookDescription = bookDescription;
            _author = author;
            _genre = genre;
            Authors = authors;
            Genres = genres;
            CancelCommand = new RelayCommand(CloseOverlay);
            SaveCommand = new RelayCommand(UpdateBook, CanCreateBook);
        }

        public BookOverlayViewModel(IServiceProvider serviceProvider, ObservableCollection<string> authors, ObservableCollection<string> genres) : base(serviceProvider) {
            bookId = Guid.Empty;
            BookTitle = string.Empty;
            BookDescription = string.Empty;
            Authors = authors;
            Genres = genres;
            CancelCommand = new RelayCommand(CloseOverlay);
            SaveCommand = new RelayCommand(CreateBook, CanCreateBook);
        }

        private bool CanCreateBook(object obj) {
            return GenreName != string.Empty && GenreName != null && AuthorName != string.Empty && AuthorName != null && BookTitle != string.Empty ;
        }

        private void UpdateBook() {
            Book book = new Book() {
                Id = bookId,
                author = serviceProvider.GetRequiredService<AuthorsModel>().GetAuthorByName(AuthorName),
                genre = serviceProvider.GetRequiredService<GenresModel>().GetGenreByName(GenreName),
                description = BookDescription ?? "Empty description",
                title = BookTitle,
            };
            CreateAction?.Invoke(book);
        }

        private void CreateBook() {
            Book book = new Book() {
                author = serviceProvider.GetRequiredService<AuthorsModel>().GetAuthorByName(AuthorName),
                genre = serviceProvider.GetRequiredService<GenresModel>().GetGenreByName(GenreName),
                description = BookDescription ?? "Empty description",
                title = BookTitle,
            };
            CreateAction?.Invoke(book);
        }

        private void CloseOverlay() {
            CancelAction?.Invoke();
        }

    }
}
