using Bookshelf.Domain.DataModels;
using Bookshelf.Infrastructure.Entities;

namespace Bookshelf.Domain.Mappers {
    public class AuthorMapper : IMapper<AuthorEntity, Author> {
        public Author Map(AuthorEntity entity) {
            if (entity == null) {
                return new Author();
            } else {
                return new Author() {
                    Id = entity.Id,
                    Name = entity.Name
                };
            }
        }

        public AuthorEntity Map(Author dataModel) {
            return new AuthorEntity() {
                Id = dataModel.Id,
                Name = dataModel.Name,
            };
        }
    }
}
