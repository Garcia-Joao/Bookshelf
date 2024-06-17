using Bookshelf.Domain.DataModels;
using Bookshelf.Infrastructure.Controllers;
using Bookshelf.Presentation.Base;
using Bookshelf.Presentation.Components;
using Bookshelf.Presentation.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Documents.Spreadsheet.Model.Filtering;

namespace Bookshelf.Presentation.ViewModels
{
    public class BooksViewModel : ViewModelBase {
        private readonly IServiceProvider _serviceProvider;
        private BooksModel booksModel;
        private List<BookCardViewModel> _allCards = new List<BookCardViewModel>();
        private ObservableCollection<BookCardViewModel> _bookCards = new ObservableCollection<BookCardViewModel>();

        public string CurrentAuthorFilter { get; private set; }
        public string CurrentGenreFilter { get; private set; }
        public string CurrentBookNameFilter { get; private set; }

        public Action<Guid> EditCardCallback { get; set; }

        public ObservableCollection<BookCardViewModel> BookCards {
            get => _bookCards;
            set {
                _bookCards = value;
                OnPropertyChanged(nameof(BookCards));
            }
        }

        public BooksViewModel(IServiceProvider serviceProvider, BooksModel booksModel) : base(serviceProvider) {
            _serviceProvider = serviceProvider;
            this.booksModel = booksModel;
            SetCards();
        }

        public void GeneralUpdate() {
            SetCards();
            RefreshCards();
        }

        private void SetCards() {
            var books = booksModel.GetAll();
            _allCards.Clear();
            foreach (var book in books) {
                var cardVM = new BookCardViewModel(_serviceProvider, book.title, book.description, book.author.Name, book.genre.Name, book.Id);
                cardVM.RemoveBookCallback += RemoveBook;
                cardVM.EditBookCallBack += EditBook;
                _allCards.Add(cardVM);
            }
            RefreshCards();
        }

        private void EditBook(Guid guid) {
            EditCardCallback?.Invoke(guid);
        }
          
        private void RemoveBook(Guid guid) {
            var bookToRemove = _allCards.FirstOrDefault(b => b.bookId == guid);
            if (bookToRemove != null) {
                _allCards.Remove(bookToRemove);
                booksModel.RemoveBook(guid);
                RefreshCards();
            }
        }

        public void RefreshCards() {
            UpdateFilters(CurrentAuthorFilter, CurrentGenreFilter, CurrentBookNameFilter);
        }

        public void UpdateFilters(string authorFilter, string genreFilter, string bookNameFilter) {
            CurrentAuthorFilter = authorFilter;
            CurrentGenreFilter = genreFilter;
            CurrentBookNameFilter = bookNameFilter;
            Filter(authorFilter, genreFilter, bookNameFilter);
        }

        internal void Filter(string authorFilter, string genreFilter, string bookNameFilter) {
            var filteredList = _allCards
                .Where(b => (string.IsNullOrEmpty(authorFilter) || b.AuthorName == authorFilter) &&
                            (string.IsNullOrEmpty(genreFilter) || b.GenreName == genreFilter) &&
                            (string.IsNullOrEmpty(bookNameFilter) || b.BookName.ToLower().Contains(bookNameFilter.ToLower())))
                .ToList();

            BookCards.Clear();
            foreach (var card in filteredList) {
                BookCards.Add(card);
            }
        }
    }

}
