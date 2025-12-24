using System.Windows.Controls;
using FreePLM.WPF.UserControls.ViewModels;

namespace FreePLM.WPF.UserControls.UserControls
{
    /// <summary>
    /// Interaction logic for CreateDocumentUserControl.xaml
    /// </summary>
    public partial class CreateDocumentUserControl : UserControl
    {
        public CreateDocumentUserControl()
        {
            InitializeComponent();
        }

        public CreateDocumentViewModel ViewModel => DataContext as CreateDocumentViewModel;
    }
}
