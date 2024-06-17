using Bookshelf.Domain.DataModels;
using Bookshelf.Domain.Mappers;
using Bookshelf.Helpers;
using Bookshelf.Infrastructure.Controllers;
using Bookshelf.Infrastructure.Domain.Controllers;
using Bookshelf.Infrastructure.Entities;

namespace Bookshelf.Domain.Services {
    public class AuthorService : Service<AuthorEntity, Author> {
        public AuthorService(Controller<AuthorEntity> controller, IMapper<AuthorEntity, Author> mapper) : base(controller, mapper) {
        }

        public Author GetRandomAuthor() {
            List<AuthorEntity> authors = controller.GetAll();
            Random rnd = new Random();
            int randomIndex = rnd.Next(authors.Count);
            return mapper.Map(authors[randomIndex]);
        }

        public List<string> GetAuthorsNames() {
            List<AuthorEntity> authors = controller.GetAll();
            return authors.Select(author => author.Name).ToList();
        }

        public Author GetAuthorByName(string name) {
            List<AuthorEntity> authors = controller.GetAll();
            return mapper.Map(authors.Where(a=> a.Name == name).First());
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

        public List<Author> GetAll() {
            List<AuthorEntity> authors = controller.GetAll();
            List<Author> returnList = new List<Author>();

            foreach (AuthorEntity author in authors) {
                returnList.Add(mapper.Map(author));
            }

            return returnList;
        }

        internal void Remove(Guid id) {
            controller.Remove(GetEntityById(id));
        }

        internal void Add(Author author) {
            controller.Add(mapper.Map(author));
        }

        internal void Update(Author author) {
            controller.Update(mapper.Map(author));
        }
    }
}
