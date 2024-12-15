using Sales_Manager.ViewModels.Pages;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Sales_Manager.Views.Pages.Workspace
{
    /// <summary>
    /// Interaction logic for Accounts.xaml
    /// </summary>
    public partial class Accounts : Page
    {
        public Accounts(AccountsViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
