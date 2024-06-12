using Bookshelf.Infrastructure.Database;
using Bookshelf.Infrastructure.Domain.Controllers;
using Bookshelf.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshelf.Infrastructure.Controllers {
    public class BookController : Controller<BookEntity> {
        public BookController(DatabaseContext context) : base(context) {
        }
    }
}
