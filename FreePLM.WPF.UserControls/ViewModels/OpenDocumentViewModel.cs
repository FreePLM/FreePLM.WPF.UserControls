using System;
using System.Windows.Input;
using FreePLM.WPF.Core.ViewModels.Base;

namespace FreePLM.WPF.UserControls.ViewModels
{
    public class OpenDocumentViewModel : ViewModelBase
    {
        private string _objectId;

        public OpenDocumentViewModel()
        {
            OpenCommand = new RelayCommand(ExecuteOpen, CanExecuteOpen);
            CancelCommand = new RelayCommand(ExecuteCancel);
        }

        public string ObjectId
        {
            get => _objectId;
            set
            {
                if (SetProperty(ref _objectId, value))
                {
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public ICommand OpenCommand { get; }
        public ICommand CancelCommand { get; }

        public event EventHandler<OpenDocumentEventArgs> OpenRequested;
        public event EventHandler CancelRequested;

        private bool CanExecuteOpen()
        {
            return !string.IsNullOrWhiteSpace(ObjectId);
        }

        private void ExecuteOpen()
        {
            if (!CanExecuteOpen())
                return;

            OpenRequested?.Invoke(this, new OpenDocumentEventArgs
            {
                ObjectId = ObjectId
            });
        }

        private void ExecuteCancel()
        {
            CancelRequested?.Invoke(this, EventArgs.Empty);
        }
    }

    public class OpenDocumentEventArgs : EventArgs
    {
        public string ObjectId { get; set; }
    }
}
