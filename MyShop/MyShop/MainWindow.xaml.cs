using MyShop.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Dashboard dashboard;
        Button[] buttons;


        private void changeButtonColor(Button b)
        {
            foreach (var button in buttons)
            {
                button.Foreground = (Brush)Application.Current.Resources["Red"];
            }
            b.Background = (Brush)Application.Current.Resources["White"];
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        bool isMaximized = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount == 2)
            {
                if(isMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1280;
                    this.Height = 780;

                    isMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                    isMaximized = true;
                }
            }
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void dashboardButton_Click(object sender, RoutedEventArgs e)
        {
            changeButtonColor(dashboardButton);
            dashboard = new Dashboard();
            pageNavigation.NavigationService.Navigate(dashboard);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            Button[] new_buttons = new Button[] { dashboardButton };
            buttons = new_buttons;
            dashboardButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
    }
}
