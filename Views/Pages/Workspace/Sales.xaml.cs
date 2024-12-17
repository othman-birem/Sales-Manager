using Sales_Manager.ViewModels.Model;
using Sales_Manager.ViewModels.Pages;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Sales_Manager.Views.Pages.Workspace
{
    /// <summary>
    /// Interaction logic for Sales.xaml
    /// </summary>
    public partial class Sales : Page
    {
        private object _draggedItem;
        private Point _startPoint;

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
        private void OrderItemsDataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition(null);

            var row = FindVisualParent<DataGridRow>((DependencyObject)e.OriginalSource);

            if (row != null)
            {
                _draggedItem = row.Item;
            }
        }
        private void OrderItemsDataGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && _draggedItem != null)
            {
                Point currentPoint = e.GetPosition(null);
                Vector difference = _startPoint - currentPoint;

                if (Math.Abs(difference.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(difference.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    DragDrop.DoDragDrop(OrderItemsDataGrid, _draggedItem, DragDropEffects.Move);
                }
            }
        }
        private void OrderItemsDataGrid_Drop(object sender, DragEventArgs e)
        {
            if (_draggedItem == null) return;

            var target = FindVisualParent<DataGridRow>((DependencyObject)e.OriginalSource)?.Item;

            if (target != null && target != _draggedItem)
            {
                var collection = OrderItemsDataGrid.ItemsSource as ObservableCollection<ItemViewModel>;

                if (collection != null)
                {
                    int oldIndex = collection.IndexOf((ItemViewModel)_draggedItem);
                    int newIndex = collection.IndexOf((ItemViewModel)target);

                    collection.Move(oldIndex, newIndex);
                }
            }
            _draggedItem = null;
        }
        private static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);

            while (parent != null && !(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as T;
        }
    }
}
