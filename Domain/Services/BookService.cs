using Bookshelf.Domain.DataModels;
using Bookshelf.Domain.Mappers;
using Bookshelf.Helpers;
using Bookshelf.Infrastructure.Domain.Controllers;
using Bookshelf.Infrastructure.Entities;

namespace Bookshelf.Domain.Services {
    public class BookService : Service<BookEntity, Book> {

        AuthorService authorService;
        GenreService genreService;

        public BookService(Controller<BookEntity> controller, IMapper<BookEntity, Book> mapper, AuthorService authorService, GenreService genreService) : base(controller, mapper) {
            this.authorService = authorService;
            this.genreService = genreService;
        }

        public List<Book> GetAllBooks() {
            var itens = controller.GetAll();
            List<Book> returnBooks = new List<Book>();

            foreach (var item in itens) {
                returnBooks.Add(mapper.Map(item));
            }
            return returnBooks;
        }

        public void InsertBookMockup() {
            string[] bookNames = JsonHelper.GetConfigurationDataArray("MockupData", "Books");
            foreach (string book in bookNames) {
                Book bookObj = new Book() {
                    title = book,
                    author = authorService.GetRandomAuthor(),
                    genre = genreService.GetRandomGenre(),
                    description = GetLoremIpsum()
                };
                Add(bookObj);
            }
        }

        internal void AddNewBook(Book bookToAdd) {
            BookEntity newBook = mapper.Map(bookToAdd);
            controller.Add(newBook);
        }

        internal Book GetBookById(Guid id) {
            return mapper.Map(controller.GetById(id));
        }

        internal void RemoveBook(Guid bookId) {
            controller.Remove(GetEntityById(bookId));
        }

        internal void UpdateBook(Book book) {
            controller.Update(mapper.Map(book));
        }

        private string GetLoremIpsum() {
            return "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus risus ipsum, pretium sit amet facilisis at, aliquet vel eros. " +
                    "Maecenas a dui elit. Nullam egestas ipsum eget pulvinar mollis. Integer nec ligula vel erat feugiat elementum sed sit amet purus. " +
                    "Maecenas a dui elit. Nullam egestas ipsum eget pulvinar mollis. Integer nec ligula vel erat feugiat elementum sed sit amet purus." +
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus risus ipsum, pretium sit amet facilisis at, aliquet vel eros.";
        }
    }
}
