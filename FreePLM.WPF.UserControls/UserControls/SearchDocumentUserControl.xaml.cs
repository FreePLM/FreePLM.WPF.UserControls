using System.Windows.Controls;
using System.Windows.Input;
using FreePLM.WPF.UserControls.ViewModels;

namespace FreePLM.WPF.UserControls.UserControls
{
    public partial class SearchDocumentUserControl : UserControl
    {
        public SearchDocumentUserControl()
        {
            InitializeComponent();
            DataContext = new SearchDocumentViewModel();
        }

        public SearchDocumentViewModel ViewModel => DataContext as SearchDocumentViewModel;

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // When user double-clicks a row, trigger the Open command
            if (ViewModel?.SelectedDocument != null && ViewModel.OpenCommand.CanExecute(null))
            {
                ViewModel.OpenCommand.Execute(null);
            }
        }
    }
}
