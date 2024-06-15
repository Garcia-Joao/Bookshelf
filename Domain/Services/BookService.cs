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

        private string GetLoremIpsum() {
            return  "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus risus ipsum, pretium sit amet facilisis at, aliquet vel eros. " +
                    "Maecenas a dui elit. Nullam egestas ipsum eget pulvinar mollis. Integer nec ligula vel erat feugiat elementum sed sit amet purus. " +
                    "Vivamus et felis tempus, fermentum turpis ac, semper dolor.";
        }
    }
}
