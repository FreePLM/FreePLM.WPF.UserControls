using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using FreePLM.WPF.Core.ViewModels.Base;

namespace FreePLM.WPF.UserControls.ViewModels
{
    public class CheckInViewModel : ViewModelBase
    {
        private string _objectId;
        private string _fileName;
        private string _currentRevision;
        private string _comment;
        private bool _createMajorRevision;
        private bool _isProcessing;
        private string _validationError;

        public CheckInViewModel()
        {
            CheckInCommand = new RelayCommand(async () => await ExecuteCheckInAsync(), CanExecuteCheckIn);
            CancelCommand = new RelayCommand(ExecuteCancel);
        }

        public string ObjectId
        {
            get => _objectId;
            set => SetProperty(ref _objectId, value);
        }

        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        public string CurrentRevision
        {
            get => _currentRevision;
            set => SetProperty(ref _currentRevision, value);
        }

        public string Comment
        {
            get => _comment;
            set
            {
                if (SetProperty(ref _comment, value))
                {
                    ValidateComment();
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public bool CreateMajorRevision
        {
            get => _createMajorRevision;
            set => SetProperty(ref _createMajorRevision, value);
        }

        public bool IsProcessing
        {
            get => _isProcessing;
            set
            {
                if (SetProperty(ref _isProcessing, value))
                {
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public string ValidationError
        {
            get => _validationError;
            set => SetProperty(ref _validationError, value);
        }

        public ICommand CheckInCommand { get; }
        public ICommand CancelCommand { get; }

        public event EventHandler<CheckInEventArgs> CheckInRequested;
        public event EventHandler CancelRequested;

        private bool CanExecuteCheckIn()
        {
            return !IsProcessing &&
                   !string.IsNullOrWhiteSpace(ObjectId) &&
                   !string.IsNullOrWhiteSpace(Comment) &&
                   string.IsNullOrEmpty(ValidationError);
        }

        private async Task ExecuteCheckInAsync()
        {
            if (!CanExecuteCheckIn())
                return;

            IsProcessing = true;
            try
            {
                CheckInRequested?.Invoke(this, new CheckInEventArgs
                {
                    ObjectId = ObjectId,
                    Comment = Comment,
                    CreateMajorRevision = CreateMajorRevision
                });
            }
            finally
            {
                IsProcessing = false;
            }
        }

        private void ExecuteCancel()
        {
            CancelRequested?.Invoke(this, EventArgs.Empty);
        }

        private void ValidateComment()
        {
            if (string.IsNullOrWhiteSpace(Comment))
            {
                ValidationError = "Comment is required for check-in.";
            }
            else if (Comment.Length < 5)
            {
                ValidationError = "Comment must be at least 5 characters long.";
            }
            else if (Comment.Length > 500)
            {
                ValidationError = "Comment must not exceed 500 characters.";
            }
            else
            {
                ValidationError = null;
            }
        }

        public void Initialize(string objectId, string fileName, string currentRevision)
        {
            ObjectId = objectId;
            FileName = fileName;
            CurrentRevision = currentRevision;
            Comment = string.Empty;
            CreateMajorRevision = false;
            ValidationError = null;
        }
    }

    public class CheckInEventArgs : EventArgs
    {
        public string ObjectId { get; set; }
        public string Comment { get; set; }
        public bool CreateMajorRevision { get; set; }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }
}
