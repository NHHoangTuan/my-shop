using MyShop.BUS;
using MyShop.DAO;
using MyShop.DTO;
using MyShop.SQLHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void rePasswordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(rePasswordTextBox.Password) && rePasswordTextBox.Password.Length > 0)
            {
                rePasswordTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                rePasswordTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void rePasswordTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            rePasswordTextBox.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string inputUsername = userTextBox.Text;
            string inputPassword = passwordTextBox.Password;
            string inputRePassword = rePasswordTextBox.Password;


            if (string.IsNullOrEmpty(inputUsername) || string.IsNullOrEmpty(inputPassword) || string.IsNullOrEmpty(inputRePassword))
                MessageBox.Show("Please enter all information");
            else if (inputPassword.Equals(inputRePassword) == false) MessageBox.Show("The 2 passwords do not match");

            else
            {

                AesHelper aesHelper = new AesHelper();
                string encryptedText = aesHelper.Encrypt(inputPassword);
                string decryptedText = aesHelper.Decrypt(encryptedText);


                User userDTO = new User
                {
                    Username = inputUsername,
                    Password = encryptedText,

                };


                UserBUS userBUS = new UserBUS();

                bool signupSucess = userBUS.createUser(userDTO);
                if (signupSucess)
                {
                    Trace.WriteLine("Sucess");
                    Button button = sender as Button;
                    Window parentWindow = Window.GetWindow(button);
                    var loginWindow = new Login();
                    loginWindow.Show();
                    parentWindow.Close();
                }
                else
                {
                    Trace.WriteLine("Failure");
                    MessageBox.Show("Failed to create new account");
                }
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
            Close();
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
    }
}
