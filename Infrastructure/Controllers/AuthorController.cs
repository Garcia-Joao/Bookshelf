using Bookshelf.Infrastructure.Database;
using Bookshelf.Infrastructure.Domain.Controllers;
using Bookshelf.Infrastructure.Entities;

namespace Bookshelf.Infrastructure.Controllers {
    public class AuthorController : Controller<AuthorEntity> {
        public AuthorController(DatabaseContext context) : base(context) {
        }
    }
}
