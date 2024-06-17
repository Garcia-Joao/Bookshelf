using Bookshelf.Domain.DataModels;
using Bookshelf.Presentation.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Bookshelf.Presentation.ViewModels {
    public class BasicCardViewModel : ViewModelBase {
        private Author author;
        private Genre genre;

        private ICommand _lButtonCommand;
        private ICommand _rButtonCommand;
        private string _lButtonText;
        private string _rButtonText;
        private string _name;
        private string _editWatermark;
        private Visibility _editVisibility;
        private Visibility _readVisibility;

        public Action<Author> RemoveAuthorCallback;
        public Action<Genre> RemoveGenreCallback;

        public Action<Author> SaveAuthorCallBack;
        public Action<Genre> SaveGenreCallBack;

        public ICommand LButtonCommand {
            get => _lButtonCommand;
            set {
                _lButtonCommand = value;
                OnPropertyChanged(nameof(LButtonCommand));
            }
        }
        public ICommand RButtonCommand {
            get => _rButtonCommand;
            set {
                _rButtonCommand = value;
                OnPropertyChanged(nameof(RButtonCommand));
            }
        }
        public string LButtonText {
            get => _lButtonText;
            set {
                _lButtonText = value;
                OnPropertyChanged(nameof(LButtonText));
            }
        }
        public string RButtonText {
            get => _rButtonText;
            set {
                _rButtonText = value;
                OnPropertyChanged(nameof(RButtonText));
            }
        }
        public string Name {
            get => _name;
            set {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string EditWatermark {
            get => _editWatermark;
            set {
                _editWatermark = value;
                OnPropertyChanged(nameof(EditWatermark));
            }
        }
        public Visibility EditVisibility {
            get => _editVisibility;
            set {
                _editVisibility = value;
                OnPropertyChanged(nameof(EditVisibility));
            }
        }

        public Visibility ReadVisibility {
            get => _readVisibility;
            set {
                _readVisibility = value;
                OnPropertyChanged(nameof(ReadVisibility));
            }
        }

        public BasicCardViewModel(IServiceProvider serviceProvider, Author author, bool editMode) : base(serviceProvider) {
            if (editMode) {
                SetEditVisibility("Author Name");
            } else {
                SetViewVisibility();
            }
            this.author = author;
            Name = author.Name;
        }

        public BasicCardViewModel(IServiceProvider serviceProvider, Genre genre, bool editMode) : base(serviceProvider) {
            if (editMode) {
                SetEditVisibility("Genre Name");
            } else {
                SetViewVisibility();
            }
            this.genre = genre;
            Name = genre.Name;
        }

        private void SetViewVisibility() {
            EditVisibility = Visibility.Collapsed;
            ReadVisibility = Visibility.Visible;
            LButtonText = "Remove";
            RButtonText = "Edit";
            LButtonCommand = new RelayCommand(SetRemoveState);
        }

        private void SetRemoveState() {
            LButtonText = "Cancel";
            RButtonText = "Confirm";
            LButtonCommand = new RelayCommand(SetViewVisibility);
            RButtonCommand = new RelayCommand(RemoveCard);
        }

        private void RemoveCard() {
            if (author != null) {
                RemoveAuthor();
            } else if (genre != null) {
                RemoveGenre();
            }
        }

        private void RemoveGenre() {
            RemoveGenreCallback?.Invoke(genre);
        }

        private void RemoveAuthor() {
            RemoveAuthorCallback?.Invoke(author);
        }

        private void SetEditVisibility(string watermarkText) {
            EditVisibility = Visibility.Visible;
            ReadVisibility = Visibility.Collapsed;
            EditWatermark = watermarkText;
            LButtonText = "Cancel";
            RButtonText = "Save";
            RButtonCommand = new RelayCommand(SaveFunction, CanSave);
        }

        private bool CanSave(object obj) {
            return Name != null && Name != string.Empty;
        }

        private void SaveFunction(object obj) {
            if (author != null) {
                SaveCallBack?.Invoke(author.Id);
            } else if (genre != null) {
                SaveCallBack?.Invoke(genre.Id);
            }
        }
    }
}
