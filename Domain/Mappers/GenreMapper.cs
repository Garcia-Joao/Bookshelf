using Bookshelf.Domain.DataModels;
using Bookshelf.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshelf.Domain.Mappers {
    public class GenreMapper : Mapper<GenreEntity, Genre> {
        public override Genre Map(GenreEntity entity) {
            if(entity == null) {
                return new Genre();
            } else {
                return new Genre {
                    Name = entity.Name,
                    Id = entity.Id,
                };
            }
        }

        public override GenreEntity Map(Genre dataModel) {
            return new GenreEntity {
                Name = dataModel.Name,
                Id = dataModel.Id,
            };
        }
    }
}
