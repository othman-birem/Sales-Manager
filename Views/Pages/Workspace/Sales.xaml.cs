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
            bool isNumeric = int.TryParse(e.Text, out int newValue);
            bool isDecimalPoint = e.Text == ".";

            TextBox textBox = (TextBox)sender;
            string currentText = textBox.Text + e.Text;

            bool isValidNumber = double.TryParse(currentText, out double result);

            e.Handled = !(isNumeric ||
                         isDecimalPoint && !textBox.Text.Contains('.') &&
                         isValidNumber && result >= 0 && result <= 100);
        }
    }
}
