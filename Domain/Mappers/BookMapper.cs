using Bookshelf.Domain.DataModels;
using Bookshelf.Domain.Services;
using Bookshelf.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bookshelf.Domain.Mappers {
    public class BookMapper : IMapper<BookEntity, Book> {
        GenreService _genreService;
        AuthorService _authorService;

        public BookMapper(GenreService genreService, AuthorService authorService) {
            _genreService = genreService;
            _authorService = authorService;
        }

        public Book Map(BookEntity entity) {
            return new Book() {
                Id = entity.Id,
                title = entity.Title,
                description = entity.Description,
                genre = _genreService.GetDatamodelById(entity.Genre_Id) ?? null,
                author = _authorService.GetDatamodelById(entity.Author_Id) ?? null,
            };
        }

        public BookEntity Map(Book dataModel) {
            return new BookEntity() {
                Id = dataModel.Id,
                Title = dataModel.title,
                Description = dataModel.description,
                Author_Id = dataModel.author.Id,
                Genre_Id = dataModel.genre.Id
            };
        }
    }
}
