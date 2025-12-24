using System;
using System.Windows.Input;
using FreePLM.WPF.Core.ViewModels.Base;
using FreePLM.WPF.Core.Commands;

namespace FreePLM.WPF.UserControls.ViewModels
{
    /// <summary>
    /// ViewModel for creating a new PLM document
    /// </summary>
    public class CreateDocumentViewModel : ViewModelBase
    {
        private string _objectId;
        private string _fileName;
        private string _revision;
        private string _project;
        private string _owner;
        private string _group;
        private string _role;
        private string _comment;
        private string _validationError;

        public CreateDocumentViewModel()
        {
            // Initialize with defaults
            Revision = "A.01";
            Project = "Default";
            Owner = "user@example.com";
            Group = "Default";
            Role = "Default";

            CreateCommand = new RelayCommand(OnCreate);
            CancelCommand = new RelayCommand(OnCancel);
        }

        #region Properties

        public string ObjectId
        {
            get => _objectId;
            set => SetProperty(ref _objectId, value);
        }

        public string FileName
        {
            get => _fileName;
            set
            {
                if (SetProperty(ref _fileName, value))
                {
                    ValidateFileName();
                }
            }
        }

        public string Revision
        {
            get => _revision;
            set => SetProperty(ref _revision, value);
        }

        public string Project
        {
            get => _project;
            set => SetProperty(ref _project, value);
        }

        public string Owner
        {
            get => _owner;
            set => SetProperty(ref _owner, value);
        }

        public string Group
        {
            get => _group;
            set => SetProperty(ref _group, value);
        }

        public string Role
        {
            get => _role;
            set => SetProperty(ref _role, value);
        }

        public string Comment
        {
            get => _comment;
            set => SetProperty(ref _comment, value);
        }

        public string ValidationError
        {
            get => _validationError;
            set => SetProperty(ref _validationError, value);
        }

        #endregion

        #region Commands

        public ICommand CreateCommand { get; }
        public ICommand CancelCommand { get; }

        #endregion

        #region Events

        public event EventHandler<CreateDocumentEventArgs> CreateRequested;
        public event EventHandler CancelRequested;

        #endregion

        #region Methods

        public void Initialize(string objectId, string fileName)
        {
            ObjectId = objectId;
            FileName = fileName;
            ValidationError = null;
        }

        private void ValidateFileName()
        {
            ValidationError = null;

            if (string.IsNullOrWhiteSpace(FileName))
            {
                ValidationError = "File name is required.";
            }
            else if (FileName.Length < 3)
            {
                ValidationError = "File name must be at least 3 characters long.";
            }
            else if (!FileName.EndsWith(".docx", StringComparison.OrdinalIgnoreCase))
            {
                ValidationError = "File name must end with .docx";
            }
        }

        private bool CanCreate()
        {
            return !string.IsNullOrWhiteSpace(FileName) &&
                   !string.IsNullOrWhiteSpace(Project) &&
                   !string.IsNullOrWhiteSpace(Owner) &&
                   !string.IsNullOrWhiteSpace(Group) &&
                   !string.IsNullOrWhiteSpace(Role) &&
                   string.IsNullOrEmpty(ValidationError);
        }

        private void OnCreate()
        {
            ValidateFileName();

            if (!string.IsNullOrEmpty(ValidationError))
            {
                return;
            }

            CreateRequested?.Invoke(this, new CreateDocumentEventArgs
            {
                ObjectId = ObjectId,
                FileName = FileName,
                Project = Project,
                Owner = Owner,
                Group = Group,
                Role = Role,
                Comment = Comment ?? "Initial document creation"
            });
        }

        private void OnCancel()
        {
            CancelRequested?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }

    public class CreateDocumentEventArgs : EventArgs
    {
        public string ObjectId { get; set; }
        public string FileName { get; set; }
        public string Project { get; set; }
        public string Owner { get; set; }
        public string Group { get; set; }
        public string Role { get; set; }
        public string Comment { get; set; }
    }
}
