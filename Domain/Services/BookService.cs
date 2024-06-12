using Bookshelf.Domain.DataModels;
using Bookshelf.Domain.Mappers;
using Bookshelf.Infrastructure.Domain.Controllers;
using Bookshelf.Infrastructure.Entities;

namespace Bookshelf.Domain.Services {
    public class BookService : Service<BookEntity, Book> {
        public BookService(Controller<BookEntity> controller, IMapper<BookEntity, Book> mapper) : base(controller, mapper) {
        }
    }
}
