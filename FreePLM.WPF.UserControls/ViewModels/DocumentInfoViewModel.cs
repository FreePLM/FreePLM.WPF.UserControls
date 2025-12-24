using System;
using FreePLM.WPF.Core.ViewModels.Base;

namespace FreePLM.WPF.UserControls.ViewModels
{
    public class DocumentInfoViewModel : ViewModelBase
    {
        private string _objectId;
        private string _fileName;
        private string _currentRevision;
        private string _status;
        private string _owner;
        private string _createdBy;
        private DateTime? _createdDate;
        private string _modifiedBy;
        private DateTime? _modifiedDate;
        private long _fileSize;
        private string _fileExtension;
        private bool _isCheckedOut;
        private string _checkedOutBy;
        private DateTime? _checkedOutDate;

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

        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        public string Owner
        {
            get => _owner;
            set => SetProperty(ref _owner, value);
        }

        public string CreatedBy
        {
            get => _createdBy;
            set => SetProperty(ref _createdBy, value);
        }

        public DateTime? CreatedDate
        {
            get => _createdDate;
            set => SetProperty(ref _createdDate, value);
        }

        public string ModifiedBy
        {
            get => _modifiedBy;
            set => SetProperty(ref _modifiedBy, value);
        }

        public DateTime? ModifiedDate
        {
            get => _modifiedDate;
            set => SetProperty(ref _modifiedDate, value);
        }

        public long FileSize
        {
            get => _fileSize;
            set => SetProperty(ref _fileSize, value);
        }

        public string FileSizeFormatted => FormatFileSize(FileSize);

        public string FileExtension
        {
            get => _fileExtension;
            set => SetProperty(ref _fileExtension, value);
        }

        public bool IsCheckedOut
        {
            get => _isCheckedOut;
            set => SetProperty(ref _isCheckedOut, value);
        }

        public string CheckedOutBy
        {
            get => _checkedOutBy;
            set => SetProperty(ref _checkedOutBy, value);
        }

        public DateTime? CheckedOutDate
        {
            get => _checkedOutDate;
            set => SetProperty(ref _checkedOutDate, value);
        }

        public string CheckOutStatusText
        {
            get
            {
                if (IsCheckedOut)
                {
                    return $"Checked out by {CheckedOutBy} on {CheckedOutDate?.ToString("g")}";
                }
                return "Available";
            }
        }

        private static string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }

        public void LoadDocument(object document)
        {
            // This method can be used to load from a PLMDocument entity
            // For now, it's a placeholder that can be implemented when needed
            if (document == null)
                return;

            var docType = document.GetType();

            ObjectId = docType.GetProperty("ObjectId")?.GetValue(document)?.ToString();
            FileName = docType.GetProperty("FileName")?.GetValue(document)?.ToString();
            CurrentRevision = docType.GetProperty("CurrentRevision")?.GetValue(document)?.ToString();
            Status = docType.GetProperty("Status")?.GetValue(document)?.ToString();
            Owner = docType.GetProperty("Owner")?.GetValue(document)?.ToString();
            CreatedBy = docType.GetProperty("CreatedBy")?.GetValue(document)?.ToString();
            CreatedDate = docType.GetProperty("CreatedDate")?.GetValue(document) as DateTime?;
            ModifiedBy = docType.GetProperty("ModifiedBy")?.GetValue(document)?.ToString();
            ModifiedDate = docType.GetProperty("ModifiedDate")?.GetValue(document) as DateTime?;
            FileSize = (long)(docType.GetProperty("FileSize")?.GetValue(document) ?? 0L);
            FileExtension = docType.GetProperty("FileExtension")?.GetValue(document)?.ToString();
            IsCheckedOut = (bool)(docType.GetProperty("IsCheckedOut")?.GetValue(document) ?? false);
            CheckedOutBy = docType.GetProperty("CheckedOutBy")?.GetValue(document)?.ToString();
            CheckedOutDate = docType.GetProperty("CheckedOutDate")?.GetValue(document) as DateTime?;

            OnPropertyChanged(nameof(FileSizeFormatted));
            OnPropertyChanged(nameof(CheckOutStatusText));
        }
    }
}
