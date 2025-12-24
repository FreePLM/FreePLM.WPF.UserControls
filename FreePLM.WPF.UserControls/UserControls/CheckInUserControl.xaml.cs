using System.Windows.Controls;
using FreePLM.WPF.UserControls.ViewModels;

namespace FreePLM.WPF.UserControls.UserControls
{
    public partial class CheckInUserControl : UserControl
    {
        public CheckInUserControl()
        {
            InitializeComponent();
            DataContext = new CheckInViewModel();
        }

        public CheckInViewModel ViewModel => DataContext as CheckInViewModel;
    }
}
