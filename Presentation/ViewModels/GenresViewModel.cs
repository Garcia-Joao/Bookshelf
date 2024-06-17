using Bookshelf.Domain.DataModels;
using Bookshelf.Presentation.Base;
using Bookshelf.Presentation.Components;
using Bookshelf.Presentation.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Documents.Spreadsheet.Model.Filtering;

namespace Bookshelf.Presentation.ViewModels {
    public class GenresViewModel : ViewModelBase {
        GenresModel genresModel;
        private ObservableCollection<BasicCardViewModel> _allCards;
        private ObservableCollection<BasicCardViewModel> _genreCards;

        public ObservableCollection<BasicCardViewModel> GenreCards {
            get => _genreCards;
            set {
                _genreCards = value;
                OnPropertyChanged(nameof(GenreCards));
            }
        }

        public GenresViewModel(IServiceProvider serviceProvider, GenresModel genresModel) : base(serviceProvider) {
            this.genresModel = genresModel;
            GenreCards = new ObservableCollection<BasicCardViewModel>();
            _allCards = new ObservableCollection<BasicCardViewModel>();
            SetCards(genresModel.GetGenresList());
        }

        private void SetCards(List<Genre> genres) {
            foreach (Genre genre in genres) {
                var card = new BasicCardViewModel(serviceProvider, genre, false);
                _allCards.Add(card);
                card.RemoveGenreCallback += RemoveGenre;
            }
            Filter(string.Empty);
        }

        private void RemoveGenre(Genre genre) {
            genresModel.Remove(genre.Id);
            UpdateCards();
        }


        private void UpdateCards() {
            _allCards.Clear();
            SetCards(genresModel.GetGenresList());
        }

        public void Filter(string filterText) {
            var filteredList = _allCards
            .Where(g => string.IsNullOrEmpty(filterText) || g.Name.ToLower().Contains(filterText.ToLower())).ToList();

            GenreCards.Clear();
            foreach (var card in filteredList) {
                GenreCards.Add(card);
            }
        }
    }
}
