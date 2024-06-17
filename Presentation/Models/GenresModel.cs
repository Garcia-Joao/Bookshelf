using Bookshelf.Domain.DataModels;
using Bookshelf.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshelf.Presentation.Models {
    public class GenresModel {
        GenreService genreService;
        public GenresModel(GenreService genreService) {
            this.genreService = genreService;
        }

        public Genre GetGenreByName(string name) {
            return genreService.GetGenreByName(name);
        }

        internal void AddGenre(Genre genre) {
            genreService.Add(genre);
        }

        internal List<Genre> GetGenresList() {
            return genreService.GetAll();
        }

        internal void Remove(Guid id) {
            genreService.Remove(id);
        }

        internal void Update(Genre genre) {
            genreService.Update(genre);
        }
    }
}
