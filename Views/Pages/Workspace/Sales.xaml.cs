using Sales_Manager.ViewModels.Pages;
using System.Windows.Controls;

namespace Sales_Manager.Views.Pages.Workspace
{
    /// <summary>
    /// Interaction logic for Sales.xaml
    /// </summary>
    public partial class Sales : Page
    {
        public Sales(SalesViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void NumericValidation(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {

        }
    }
}
