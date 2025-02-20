﻿using MyShop.Config;
using MyShop.SQLHelper;
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
using System.Windows.Shapes;

namespace MyShop
{
    /// <summary>
    /// Interaction logic for ConfigDataBase.xaml
    /// </summary>
    public partial class ConfigDataBase : Window
    {
        public ConfigDataBase()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string servername = ServerTermTextBox.Text;
            string password = PasswordTermTextBox.Text;
            string username = UsernameTermTextBox.Text;
            string databasename = DatabaseTermTextBox.Text;

            if (string.IsNullOrEmpty(servername) || string.IsNullOrEmpty(databasename) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter all information");
                return;
            }
                

            AppConfig.SetValue("server", servername);
            AppConfig.SetValue("database", databasename);
            AppConfig.SetValue("usernameSQL", username);
            AppConfig.SetValue("passwordSQL", password);

            new SQLData(
                  
            );

            if (SQLData.isSelectedDatabase == true)
            {
                Login loginWindow = new Login();
                loginWindow.Show();
                this.Close();
            }
        }
    }
}
