using Bookshelf.Presentation.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bookshelf.Presentation.ViewModels {
    public class BookCardViewModel : ViewModelBase {

        private string _rButtonText;
        private string _lButtonText;

        private string _bookName;
        private string _bookDescription;
        private string _authorName;
        private string _genreName;

        public readonly Guid bookId;

        public string RButtonText {
            get => _rButtonText;
            set {
                _rButtonText = value;
                OnPropertyChanged(nameof(RButtonText));
            }
        }

        public string LButtonText {
            get => _lButtonText;
            set {
                _lButtonText = value;
                OnPropertyChanged(nameof(LButtonText));
            }
        }

        public string BookName {
            get { return _bookName; }
            set {
                _bookName = value;
                OnPropertyChanged(nameof(BookName));
            }
        }

        public string BookDescription {
            get { return _bookDescription; }
            set {
                _bookDescription = value;
                OnPropertyChanged(nameof(BookDescription));
            }
        }

        public string AuthorName {
            get { return _authorName; }
            set {
                _authorName = value;
                OnPropertyChanged(nameof(AuthorName));
            }
        }

        public string GenreName {
            get { return _genreName; }
            set {
                _genreName = value;
                OnPropertyChanged(nameof(GenreName));
            }
        }

        private ICommand _lButtonCommand;
        private ICommand _rButtonCommand;

        public ICommand LButtonCommand { 
            get => _lButtonCommand;
            private set {
                _lButtonCommand = value;
                OnPropertyChanged(nameof(LButtonCommand));
            } 
        }

        public ICommand RButtonCommand {
            get => _rButtonCommand;
            private set {
                _rButtonCommand = value;
                OnPropertyChanged(nameof(RButtonCommand));
            }
        }

        public Action<Guid> RemoveBookCallback;
        public Action<Guid> EditBookCallBack;

        public BookCardViewModel(IServiceProvider serviceProvider, string BookName, string BookDescription, string AuthorName, string GenreName, Guid bookId) : base(serviceProvider) {
            this.BookName = BookName;
            this.BookDescription = BookDescription;
            this.AuthorName = AuthorName;
            this.GenreName = GenreName;
            this.bookId = bookId;
            SetDefaultState();
        }

        private void SetDefaultState() {
            LButtonText = "Remove";
            RButtonText = "Edit";
            LButtonCommand = new RelayCommand(SetRemoveState);
            RButtonCommand = new RelayCommand(SetEditState);
        }

        private void SetRemoveState() {
            LButtonText = "Cancel";
            RButtonText = "Confirm";
            LButtonCommand = new RelayCommand(SetDefaultState);
            RButtonCommand = new RelayCommand(RemoveCard);
       }

        private void RemoveCard(object obj) {
            RemoveBookCallback?.Invoke(bookId);
        }

        private void SetEditState(object obj) {
            EditBookCallBack?.Invoke(bookId);
        }
    }
}
