using Bookshelf.Domain.DataModels;
using Bookshelf.Domain.Mappers;
using Bookshelf.Helpers;
using Bookshelf.Infrastructure.Domain.Controllers;
using Bookshelf.Infrastructure.Entities;

namespace Bookshelf.Domain.Services {
    public class AuthorService : Service<AuthorEntity, Author> {
        public AuthorService(Controller<AuthorEntity> controller, IMapper<AuthorEntity, Author> mapper) : base(controller, mapper) {
        }

        public void InsertAuthorMockups() {
            string[] authors = JsonHelper.GetConfigurationDataArray("MockupData", "Authors");
            foreach (string author in authors) {
                Author authorObj = new Author() {
                    Name = author
                };
                Add(authorObj);
            }
        }
    }
}
