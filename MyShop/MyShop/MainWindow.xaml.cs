using MahApps.Metro.IconPacks;
using MyShop.Config;
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
        Dashboard dashboard;
        ManageCategory manageCategories;
        ManageProducts manageProducts;
        ManageOrders manageOrders;
        Statistic statistic;
        Configuration configuration;
        ManagePromotion promotion;
        ToggleButton[] buttons;

        public MainWindow()
        {
            InitializeComponent();
            dashboard = new Dashboard();
            manageCategories = new ManageCategory();
            manageProducts = new ManageProducts();
            manageOrders = new ManageOrders();
            statistic = new Statistic(pageNavigation);
            configuration = new Configuration();
            promotion = new ManagePromotion();
           
        }

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
          
            ToggleButton[] new_buttons = new ToggleButton[] { dashboardButton, managecategoriesButton, manageproductsButton, 
                manageordersButton, statisticButton, configurationButton, promotionButton};
            buttons = new_buttons;
            if (AppConfig.GetValue("StartUpPage") == "Dashboard")
            {
                dashboardButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (AppConfig.GetValue("StartUpPage") == "ManageProduct")
            {
                manageproductsButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (AppConfig.GetValue("StartUpPage") == "ManageCategory")
            {
                managecategoriesButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (AppConfig.GetValue("StartUpPage") == "ManageOrder")
            {
                manageordersButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (AppConfig.GetValue("StartUpPage") == "Statistic")
            {
                statisticButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (AppConfig.GetValue("StartUpPage") == "Configuration")
            {
                configurationButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else if (AppConfig.GetValue("StartUpPage") == "Promotion")
            {
                promotionButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }

        }

        private void dashboardButton_Click(object sender, RoutedEventArgs e)
        {
            changeButtonColor(dashboardButton);
            if(pageNavigation.NavigationService.Content!=null)
            {
                pageNavigation.NavigationService.RemoveBackEntry();
            }
            
            pageNavigation.NavigationService.Navigate(dashboard);
            AppConfig.SetValue("StartUpPage", "Dashboard");
        }

       

        private void manageproductsButton_Click(object sender, RoutedEventArgs e)
        {
            changeButtonColor(manageproductsButton);
            if (pageNavigation.NavigationService.Content != null)
            {
                pageNavigation.NavigationService.RemoveBackEntry();
            }
           
            pageNavigation.NavigationService.Navigate(manageProducts);
            AppConfig.SetValue("StartUpPage", "ManageProduct");
        }

        private void manageordersButton_Click(object sender, RoutedEventArgs e)
        {
            changeButtonColor(manageordersButton);
            if (pageNavigation.NavigationService.Content != null)
            {
                pageNavigation.NavigationService.RemoveBackEntry();
            }
            
            pageNavigation.NavigationService.Navigate(manageOrders);
            AppConfig.SetValue("StartUpPage", "ManageOrder");
        }

        private void statisticButton_Click(object sender, RoutedEventArgs e)
        {
            changeButtonColor(statisticButton);
            if (pageNavigation.NavigationService.Content != null)
            {
                pageNavigation.NavigationService.RemoveBackEntry();
            }
           
            pageNavigation.NavigationService.Navigate(statistic);
            AppConfig.SetValue("StartUpPage", "Statistic");
        }

        private void configurationButton_Click(object sender, RoutedEventArgs e)
        {
            changeButtonColor(configurationButton);
            if (pageNavigation.NavigationService.Content != null)
            {
                pageNavigation.NavigationService.RemoveBackEntry();
            }
            
            pageNavigation.NavigationService.Navigate(configuration);
            AppConfig.SetValue("StartUpPage", "Configuration");
        }

        private void managecategoriesButton_Click(object sender, RoutedEventArgs e)
        {
            changeButtonColor(managecategoriesButton);
            if (pageNavigation.NavigationService.Content != null)
            {
                pageNavigation.NavigationService.RemoveBackEntry();
            }
          
            pageNavigation.NavigationService.Navigate(manageCategories);
            AppConfig.SetValue("StartUpPage", "ManageCategory");

        }

        private void promotionButton_Click(object sender, RoutedEventArgs e)
        {
            changeButtonColor(promotionButton);
            if (pageNavigation.NavigationService.Content != null)
            {
                pageNavigation.NavigationService.RemoveBackEntry();
            }
         
            pageNavigation.NavigationService.Navigate(promotion);
            AppConfig.SetValue("StartUpPage", "Promotion");
        }
    }
}
