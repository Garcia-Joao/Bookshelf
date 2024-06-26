﻿using Bookshelf.Domain.DataModels;
using Bookshelf.Domain.Services;
using Bookshelf.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bookshelf.Domain.Mappers {
    public class BookMapper : Mapper<BookEntity, Book> {
        GenreService _genreService;
        AuthorService _authorService;

        public BookMapper(GenreService genreService, AuthorService authorService) {
            _genreService = genreService;
            _authorService = authorService;
        }

        public override Book Map(BookEntity entity) {
            return new Book() {
                Id = entity.Id,
                title = entity.Title,
                description = entity.Description!,
                genre = _genreService.GetDatamodelById(entity.Genre_Id),
                author = _authorService.GetDatamodelById(entity.Author_Id),
            };
        }

        public override BookEntity Map(Book dataModel) {
            return new BookEntity() {
                Id = dataModel.Id,
                Title = dataModel.title,
                Description = dataModel.description,
                Author_Id = dataModel.author!.Id,
                Genre_Id = dataModel.genre!.Id
            };
        }
    }
}
