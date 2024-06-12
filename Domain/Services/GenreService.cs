using Bookshelf.Domain.DataModels;
using Bookshelf.Domain.Mappers;
using Bookshelf.Infrastructure.Controllers;
using Bookshelf.Infrastructure.Domain.Controllers;
using Bookshelf.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshelf.Domain.Services {
    public class GenreService : Service<GenreEntity, Genre> {
        public GenreService(GenreController controller, GenreMapper mapper) : base(controller, mapper) {
        }
    }
}
