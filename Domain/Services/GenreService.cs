using Bookshelf.Domain.DataModels;
using Bookshelf.Domain.Mappers;
using Bookshelf.Helpers;
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
        public GenreService(Controller<GenreEntity> controller, IMapper<GenreEntity, Genre> mapper) : base(controller, mapper) {
        }

        public List<string> GetGenresNames() {
            List<GenreEntity> genres = controller.GetAll();
            return genres.Select(author => author.Name).ToList();
        }

        public void InsertGenreMockups() {
            string[] genres = JsonHelper.GetConfigurationDataArray("MockupData", "Genres");
            foreach (string genre in genres) {
                Genre genreObj = new Genre() {
                    Name = genre
                };
                Add(genreObj);
            }
        }

        public Genre GetRandomGenre() {
            List<GenreEntity> genres = controller.GetAll();
            Random rnd = new Random();
            int randomIndex = rnd.Next(genres.Count);
            return mapper.Map(genres[randomIndex]);
        }

        internal Genre GetGenreByName(string name) {
            List<GenreEntity> genres = controller.GetAll();
            return mapper.Map(genres.Where(g => g.Name == name).First());
        }

        internal List<Genre> GetAll() {
            var all = controller.GetAll().ToList();
            List<Genre> returnList = new List<Genre>();
            foreach (var genre in all) {
                returnList.Add(mapper.Map(genre));
            }
            return returnList;
        }

        internal void Remove(Guid id) {
            controller.Remove(GetEntityById(id));
        }

        internal void Add(Genre genre) {
            controller.Add(mapper.Map(genre));
        }

        internal void Update(Genre genre) {
            controller.Update(mapper.Map(genre));
        }
    }
}
