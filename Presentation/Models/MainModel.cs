using Bookshelf.Domain.DataModels;
using Bookshelf.Domain.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace Bookshelf.Presentation.Models
{
    public class MainModel
    {
        AuthorService authorService;
        GenreService genreService;
        BookService bookService;

        public MainModel(AuthorService authorService, GenreService genreService, BookService bookService) {
            this.bookService = bookService;
            this.authorService = authorService;
            this.genreService = genreService;
        }

        public Book GetBookById(Guid id) {
            return bookService.GetBookById(id);
        }

        public List<Author> GetAuthors() {
            return authorService.GetAll();
        }

        public ObservableCollection<string> GetAuthorsNames() {
            ObservableCollection<string> authorsNames = new ObservableCollection<string>(
                authorService.GetAuthorsNames().OrderBy(name => name)
            );
            return authorsNames;
        }

        public ObservableCollection<string> GetGenresNames() {
            var genresNames = new ObservableCollection<string>(
                genreService.GetGenresNames().OrderBy(name => name)
            );
            return genresNames;
        }
    }
}
