using MahApps.Metro.IconPacks;
using MyShop.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        ManageProducts manageProducts;
        ManageOrders manageOrders;
        Statistic statistic;
        Configuration configuration;
        ToggleButton[] buttons;
        


        private void changeButtonColor(ToggleButton b)
        {

            foreach(var button in buttons)
            {
            
               button.IsChecked = false;

            }
            
            b.IsChecked = true;
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


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
          
            ToggleButton[] new_buttons = new ToggleButton[] { dashboardButton, manageproductsButton, manageordersButton, statisticButton, configurationButton};
            buttons = new_buttons;
            dashboardButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

        }

        private void dashboardButton_Click(object sender, RoutedEventArgs e)
        {
            changeButtonColor(dashboardButton);
            if(pageNavigation.NavigationService.Content!=null)
            {
                pageNavigation.NavigationService.RemoveBackEntry();
            }
            dashboard = new Dashboard();
            pageNavigation.NavigationService.Navigate(dashboard);
        }

       

        private void manageproductsButton_Click(object sender, RoutedEventArgs e)
        {
            changeButtonColor(manageproductsButton);
            if (pageNavigation.NavigationService.Content != null)
            {
                pageNavigation.NavigationService.RemoveBackEntry();
            }
            manageProducts = new ManageProducts();
            pageNavigation.NavigationService.Navigate(manageProducts);
        }

        private void manageordersButton_Click(object sender, RoutedEventArgs e)
        {
            changeButtonColor(manageordersButton);
            if (pageNavigation.NavigationService.Content != null)
            {
                pageNavigation.NavigationService.RemoveBackEntry();
            }
            manageOrders = new ManageOrders();
            pageNavigation.NavigationService.Navigate(manageOrders);
        }

        private void statisticButton_Click(object sender, RoutedEventArgs e)
        {
            changeButtonColor(statisticButton);
            if (pageNavigation.NavigationService.Content != null)
            {
                pageNavigation.NavigationService.RemoveBackEntry();
            }
            statistic = new Statistic();
            pageNavigation.NavigationService.Navigate(statistic);
        }

        private void configurationButton_Click(object sender, RoutedEventArgs e)
        {
            changeButtonColor(configurationButton);
            if (pageNavigation.NavigationService.Content != null)
            {
                pageNavigation.NavigationService.RemoveBackEntry();
            }
            configuration = new Configuration();
            pageNavigation.NavigationService.Navigate(configuration);
        }
    }
}
