using System.Windows.Controls;
using FreePLM.WPF.UserControls.ViewModels;

namespace FreePLM.WPF.UserControls.UserControls
{
    public partial class DocumentInfoUserControl : UserControl
    {
        public DocumentInfoUserControl()
        {
            InitializeComponent();
            DataContext = new DocumentInfoViewModel();
        }

        public DocumentInfoViewModel ViewModel => DataContext as DocumentInfoViewModel;
    }
}
