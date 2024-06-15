using Bookshelf.Infrastructure.Database;
using Bookshelf.Infrastructure.Domain.Controllers;
using Bookshelf.Infrastructure.Entities;

namespace Bookshelf.Infrastructure.Controllers {
    public class GenreController : Controller<GenreEntity> {
        public GenreController(DatabaseContext context) : base(context) {
        }
    }
}
