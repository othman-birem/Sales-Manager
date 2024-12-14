using Sales_Manager.ViewModels.Pages;
using System.Windows.Controls;

namespace Sales_Manager.Views.Pages.Workspace
{
    /// <summary>
    /// Interaction logic for Customers.xaml
    /// </summary>
    public partial class Customers : Page
    {
        public Customers(CustomersViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
