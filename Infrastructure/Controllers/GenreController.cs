using Bookshelf.Infrastructure.Database;
using Bookshelf.Infrastructure.Domain.Controllers;
using Bookshelf.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshelf.Infrastructure.Controllers {
    public class GenreController : Controller<GenreEntity> {
        public GenreController(DatabaseContext context) : base(context) {
        }
    }
}
