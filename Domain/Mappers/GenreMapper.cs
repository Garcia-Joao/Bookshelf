using Bookshelf.Domain.DataModels;
using Bookshelf.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshelf.Domain.Mappers {
    public class GenreMapper : IMapper<GenreEntity, Genre> {
        public Genre Map(GenreEntity entity) {
            return new Genre {
                Name = entity.Name,
            };
        }

        public GenreEntity Map(Genre dataModel) {
            return new GenreEntity {
                Name = dataModel.Name,
            };
        }
    }
}
