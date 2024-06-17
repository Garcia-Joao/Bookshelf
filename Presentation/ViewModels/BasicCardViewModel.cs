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

        Datamodel currentDataModel;

        private ICommand _lButtonCommand;
        private ICommand _rButtonCommand;
        private string _lButtonText;
        private string _rButtonText;
        private string _name;
        private string _editWatermark;
        private Visibility _editVisibility;
        private Visibility _readVisibility;

        public Action<Datamodel> RemoveCallback;
        public Action<Datamodel> EditCallback;

        public Action<Datamodel> SaveCallBack;
        public Action CancelCallback;


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
                if(currentDataModel is Genre genreData) {
                    genreData.Name = value;
                }else if(currentDataModel is Author authorData) {
                    authorData.Name = value;
                }
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

        public BasicCardViewModel(IServiceProvider serviceProvider, Datamodel datamodel, bool editMode) : base(serviceProvider) {
            currentDataModel = datamodel;
            if (editMode) {
                SetEditState();
            } else {
                SetReadState();
            }

            if (datamodel is Author author) {
                Name = author.Name;
            } else if (datamodel is Genre genre) {
                Name = genre.Name;
            }
        }

        private void SetReadState() {
            EditVisibility = Visibility.Collapsed;
            ReadVisibility = Visibility.Visible;
            LButtonText = "Remove";
            RButtonText = "Edit";

            RButtonCommand = new RelayCommand(EditConfirmed);
            LButtonCommand = new RelayCommand(SetRemoveState);
        }

        private void SetRemoveState(object obj) {
            LButtonText = "Cancel";
            RButtonText = "Confirm";
            
            LButtonCommand = new RelayCommand(SetReadState);
            RButtonCommand = new RelayCommand(RemovalConfirmed);
        }

        private void SetEditState() {
            if (currentDataModel is Genre)
                EditWatermark = "Genre Name";
            else if (currentDataModel is Author)
                EditWatermark = "Author Name";

            ReadVisibility = Visibility.Collapsed;
            EditVisibility = Visibility.Visible;
            LButtonText = "Cancel";
            RButtonText = "Save";

            LButtonCommand = new RelayCommand(CancelConfirmed);
            RButtonCommand = new RelayCommand(SaveConfirmed, CanSave);
        }

        private void CancelConfirmed() {
            CancelCallback?.Invoke();
        }

        private void SaveConfirmed() {
            SaveCallBack?.Invoke(currentDataModel);
        }

        private void EditConfirmed() {
            EditCallback?.Invoke(currentDataModel);
        }

        private void RemovalConfirmed() {
            RemoveCallback?.Invoke(currentDataModel);
        }

        private bool CanSave(object obj) {
            return Name != null && Name != string.Empty;
        }
    }
}
