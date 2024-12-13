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
        }

        private void MainFrame_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            Page frame = (Page)((Frame)sender).Content;
            if (frame == null) return;
            frame.Opacity = 0;

            Storyboard storyboard = new();
            DoubleAnimation animation = new()
            {
                Duration = new Duration(TimeSpan.FromMilliseconds(100)),
                From = 1,
                To = 0
            };

            storyboard.Children.Add(animation);

            Storyboard.SetTarget(animation, frame);
            Storyboard.SetTargetProperty(animation, new PropertyPath(Page.OpacityProperty));

            storyboard.Begin();
        }
    }
}