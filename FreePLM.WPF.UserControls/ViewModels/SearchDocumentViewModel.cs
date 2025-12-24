using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FreePLM.WPF.Core.ViewModels.Base;

namespace FreePLM.WPF.UserControls.ViewModels
{
    public class SearchDocumentViewModel : ViewModelBase
    {
        private string _searchObjectId;
        private string _searchFileName;
        private string _searchProject;
        private string _searchOwner;
        private string _searchStatus;
        private bool _isSearching;
        private string _statusMessage;
        private DocumentSearchResult _selectedDocument;

        public SearchDocumentViewModel()
        {
            SearchCommand = new RelayCommand(async () => await ExecuteSearchAsync(), CanExecuteSearch);
            OpenCommand = new RelayCommand(ExecuteOpen, CanExecuteOpen);
            CancelCommand = new RelayCommand(ExecuteCancel);
            ClearCommand = new RelayCommand(ExecuteClear);

            SearchResults = new ObservableCollection<DocumentSearchResult>();
        }

        public string SearchObjectId
        {
            get => _searchObjectId;
            set
            {
                if (SetProperty(ref _searchObjectId, value))
                {
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public string SearchFileName
        {
            get => _searchFileName;
            set
            {
                if (SetProperty(ref _searchFileName, value))
                {
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public string SearchProject
        {
            get => _searchProject;
            set
            {
                if (SetProperty(ref _searchProject, value))
                {
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public string SearchOwner
        {
            get => _searchOwner;
            set
            {
                if (SetProperty(ref _searchOwner, value))
                {
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public string SearchStatus
        {
            get => _searchStatus;
            set
            {
                if (SetProperty(ref _searchStatus, value))
                {
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public bool IsSearching
        {
            get => _isSearching;
            set
            {
                if (SetProperty(ref _isSearching, value))
                {
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        public DocumentSearchResult SelectedDocument
        {
            get => _selectedDocument;
            set
            {
                if (SetProperty(ref _selectedDocument, value))
                {
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public ObservableCollection<DocumentSearchResult> SearchResults { get; }

        public ICommand SearchCommand { get; }
        public ICommand OpenCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand ClearCommand { get; }

        public event EventHandler<DocumentSearchEventArgs> SearchRequested;
        public event EventHandler<DocumentSearchEventArgs> OpenRequested;
        public event EventHandler CancelRequested;

        private bool CanExecuteSearch()
        {
            return !IsSearching &&
                   (!string.IsNullOrWhiteSpace(SearchObjectId) ||
                    !string.IsNullOrWhiteSpace(SearchFileName) ||
                    !string.IsNullOrWhiteSpace(SearchProject) ||
                    !string.IsNullOrWhiteSpace(SearchOwner) ||
                    !string.IsNullOrWhiteSpace(SearchStatus));
        }

        private bool CanExecuteOpen()
        {
            return !IsSearching && SelectedDocument != null;
        }

        private async Task ExecuteSearchAsync()
        {
            if (!CanExecuteSearch())
                return;

            IsSearching = true;
            StatusMessage = "Searching...";
            SearchResults.Clear();

            try
            {
                SearchRequested?.Invoke(this, new DocumentSearchEventArgs
                {
                    ObjectId = SearchObjectId,
                    FileName = SearchFileName,
                    Project = SearchProject,
                    Owner = SearchOwner,
                    Status = SearchStatus
                });
            }
            finally
            {
                IsSearching = false;
            }
        }

        private void ExecuteOpen()
        {
            if (!CanExecuteOpen())
                return;

            OpenRequested?.Invoke(this, new DocumentSearchEventArgs
            {
                ObjectId = SelectedDocument.ObjectId,
                FileName = SelectedDocument.FileName
            });
        }

        private void ExecuteCancel()
        {
            CancelRequested?.Invoke(this, EventArgs.Empty);
        }

        private void ExecuteClear()
        {
            SearchObjectId = string.Empty;
            SearchFileName = string.Empty;
            SearchProject = string.Empty;
            SearchOwner = string.Empty;
            SearchStatus = string.Empty;
            SearchResults.Clear();
            StatusMessage = string.Empty;
            SelectedDocument = null;
        }

        public void SetSearchResults(System.Collections.Generic.IEnumerable<DocumentSearchResult> results)
        {
            SearchResults.Clear();

            if (results == null || !results.Any())
            {
                StatusMessage = "No documents found.";
                return;
            }

            foreach (var result in results)
            {
                SearchResults.Add(result);
            }

            StatusMessage = $"Found {SearchResults.Count} document(s).";
        }
    }

    public class DocumentSearchResult
    {
        public string ObjectId { get; set; }
        public string FileName { get; set; }
        public string Revision { get; set; }
        public string Status { get; set; }
        public string Owner { get; set; }
        public string Project { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsCheckedOut { get; set; }
        public string CheckedOutBy { get; set; }
    }

    public class DocumentSearchEventArgs : EventArgs
    {
        public string ObjectId { get; set; }
        public string FileName { get; set; }
        public string Project { get; set; }
        public string Owner { get; set; }
        public string Status { get; set; }
    }
}
