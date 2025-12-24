using System.Windows.Controls;
using FreePLM.WPF.UserControls.ViewModels;

namespace FreePLM.WPF.UserControls.UserControls
{
    public partial class OpenDocumentUserControl : UserControl
    {
        public OpenDocumentUserControl()
        {
            InitializeComponent();
            DataContext = new OpenDocumentViewModel();
        }

        public OpenDocumentViewModel ViewModel => DataContext as OpenDocumentViewModel;
    }
}
