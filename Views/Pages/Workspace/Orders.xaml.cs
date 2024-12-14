using Sales_Manager.ViewModels.Pages;
using System.Windows.Controls;

namespace Sales_Manager.Views.Pages.Workspace
{
    /// <summary>
    /// Interaction logic for Orders.xaml
    /// </summary>
    public partial class Orders : Page
    {
        public Orders(OrdersViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
