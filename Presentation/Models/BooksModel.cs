using Bookshelf.Domain.DataModels;
using Bookshelf.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshelf.Presentation.Models {
    public class BooksModel {
        private BookService bookService;
        public BooksModel(BookService bookService) {
            this.bookService = bookService;
        }

        public List<Book> GetAllBooks() {
            return bookService.GetAllBooks().OrderBy(b => b.title).ToList();
        }

        internal void AddBook(Book bookToAdd) {
            bookService.AddNewBook(bookToAdd);
        }

        internal List<Book> GetAll() {
            return bookService.GetAllBooks();
        }

        internal void RemoveBook(Guid bookId) {
            bookService.RemoveBook(bookId);
        }

        internal void UpdateBook(Book book) {
            bookService.UpdateBook(book);
        }
    }
}
