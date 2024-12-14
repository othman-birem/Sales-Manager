using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Sales_Manager.Views.Navigation
{
    /// <summary>
    /// Interaction logic for NavigationSideBar.xaml
    /// </summary>
    public partial class NavigationSideBar : UserControl
    {
        public NavigationSideBar()
        {
            InitializeComponent();
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Storyboard storyboard = new();
            CircleEase ease = new() { EasingMode = EasingMode.EaseOut };

            DoubleAnimation animation = new()
            {
                EasingFunction = ease,
                Duration = new Duration(TimeSpan.FromMilliseconds(300)),
                From = (Boolean)e.NewValue ? 0.0 : 80.0,
                To = (Boolean)e.NewValue ? 80.0 : 0.0
            };

            storyboard.Children.Add(animation);

            Storyboard.SetTarget(animation, this);
            Storyboard.SetTargetProperty(animation, new PropertyPath(UserControl.WidthProperty));

            storyboard.Begin();
        }
    }
}
