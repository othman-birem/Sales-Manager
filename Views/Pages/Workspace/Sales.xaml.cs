using Sales_Manager.ViewModels.Pages;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        readonly Regex NumberRegex = new(@"^\d*\.?\d*$");
        private void NumericValidation(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string currentText = textBox.Text;

            string predictedText = currentText.Insert(textBox.SelectionStart, e.Text);

            e.Handled = !NumberRegex.IsMatch(predictedText);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (decimal.TryParse(textBox.Text, out decimal value))
            {
                if (value < 0 || value > 100)
                {
                    textBox.Text = value < 0 ? "0" : "100";
                    textBox.CaretIndex = textBox.Text.Length;
                }
            }
            else if (!string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = string.Empty;
            }
        }

        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter) DiscountBox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }
    }
}
