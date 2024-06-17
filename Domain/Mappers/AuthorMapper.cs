using Bookshelf.Domain.DataModels;
using Bookshelf.Infrastructure.Entities;
using Telerik.Windows.Documents.UI.Layers;

namespace Bookshelf.Domain.Mappers {
    public class AuthorMapper : Mapper<AuthorEntity, Author> {
        public override Author Map(AuthorEntity entity) {
            if (entity == null) {
                return new Author();
            } else {
                return new Author() {
                    Id = entity.Id,
                    Name = entity.Name
                };
            }
        }

        public override AuthorEntity Map(Author dataModel) {
            return new AuthorEntity() {
                Id = dataModel.Id,
                Name = dataModel.Name,
            };
        }
    }
}
