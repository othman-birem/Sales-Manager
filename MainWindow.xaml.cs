using Sales_Manager.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

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
            viewModel.NavigationSidebarViewModel.Navigated += (e) => OnNavigated(e);

            navbar.DataContext = viewModel.NavigationSidebarViewModel;
        }

        private void OnNavigated(object  e)
        {
            switch (int.Parse(e.ToString()))
            {
                //    case 1:
                //        navbar.SalesButton.IsChecked = true;
                //        break;
                //    case 2:
                //        navbar.OrdersButton.IsChecked = true;
                //        break;
                //    case 3:
                //        navbar.CustomersButton.IsChecked = true;
                //        break;
                //    case 4:
                //        navbar.AccountsButton.IsChecked = true;
                //        break;
                //    case 5:
                //        navbar.SettingButton.IsChecked = true;
                //        break;
            }
        }

        private void MainFrame_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            //Page frame = (Page)((Frame)sender).Content;
            //if (frame == null) return;
            //frame.Opacity = 0;

            //Storyboard storyboard = new();
            //DoubleAnimation animation = new()
            //{
            //    Duration = new Duration(TimeSpan.FromMilliseconds(100)),
            //    From = 1,
            //    To = 0
            //};

            //storyboard.Children.Add(animation);

            //Storyboard.SetTarget(animation, frame);
            //Storyboard.SetTargetProperty(animation, new PropertyPath(Page.OpacityProperty));

            //storyboard.Begin();
        }
    }
}