using Bookshelf.Domain.DataModels;
using Bookshelf.Presentation.Base;
using Bookshelf.Presentation.Models;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshelf.Presentation.ViewModels {
    public class AuthorsViewModel : ViewModelBase {
        AuthorsModel authorsModel;
        private ObservableCollection<BasicCardViewModel> _allCards;
        private ObservableCollection<BasicCardViewModel> _authorCards;

        public Action<Author> editAuthorCallback;

        public ObservableCollection<BasicCardViewModel> AuthorCards {
            get => _authorCards;
            set {
                _authorCards = value;
                OnPropertyChanged(nameof(AuthorCards));
            }
        }

        public AuthorsViewModel(IServiceProvider serviceProvider, AuthorsModel authorsModel) : base(serviceProvider) {
            this.authorsModel = authorsModel;
            _allCards = new ObservableCollection<BasicCardViewModel>();
            AuthorCards = new ObservableCollection<BasicCardViewModel>();
            SetCards(authorsModel.GetAuthorsList());
        }

        public void SetCards(List<Author> authors) {
            foreach (Author author in authors) {
                var card = new BasicCardViewModel(serviceProvider, author, false);
                _allCards.Add(card);
                card.RemoveCallback += RemoveAuthor;
                card.EditCallback += EditAuthor;
            }
            Filter(string.Empty);
        }

        private void EditAuthor(Datamodel datamodel) {
            editAuthorCallback?.Invoke(datamodel as Author);
        }

        private void RemoveAuthor(Datamodel author) {
            authorsModel.Remove(author.Id);
            UpdateCards();
        }

        private void UpdateCards() {
            _allCards.Clear();
            SetCards(authorsModel.GetAuthorsList());
        }

        public void Filter(string filterText) {
            var filteredList = _allCards.Where(a => string.IsNullOrEmpty(filterText) || a.Name.ToLower().Contains(filterText.ToLower())).ToList();
            AuthorCards.Clear();
            foreach (var card in filteredList) {
                AuthorCards.Add(card);
            }
        }

        internal void CreateNewAuthor(Author author) {
            authorsModel.Add(author);
            UpdateCards();
        }

        internal void UpdateAuthor(Author? author) {
            authorsModel.Update(author);
            UpdateCards();
        }
    }
}
