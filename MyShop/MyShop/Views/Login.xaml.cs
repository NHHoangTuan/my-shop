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
using System.Windows.Shapes;

namespace MyShop.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void userTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            userTextBox.Focus();
        }

        private void userTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(userTextBox.Text) && userTextBox.Text.Length > 0)
            {
                userTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                userTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void passwordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(passwordTextBox.Password) && passwordTextBox.Password.Length > 0)
            {
                passwordTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                passwordTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void passwordTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordTextBox.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(userTextBox.Text) && !string.IsNullOrEmpty(passwordTextBox.Password))
            {

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
