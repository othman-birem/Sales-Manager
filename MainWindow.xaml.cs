using Sales_Manager.ViewModels;
using System.Windows;

namespace Sales_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel viewModel;
        public MainWindow(MainWindowViewModel VM)
        {
            InitializeComponent();
            DataContext = VM;
            viewModel = VM;
        }
    }
}