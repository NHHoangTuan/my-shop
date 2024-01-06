using MyShop.Config;
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

namespace MyShop.Views
{
    /// <summary>
    /// Interaction logic for Configuration.xaml
    /// </summary>
    public partial class Configuration : Page
    {

        private string nPhonePerPage = "5";
        private bool isChecked = false;

        public Configuration()
        {
            InitializeComponent();
        }


        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            var item = (ComboBoxItem)nProductComboBox.SelectedValue;

            var content = "";

            if (item != null)
            {
                content = (string)item.Content;
            }

            if (content != "")
                AppConfig.SetValue(AppConfig.NumberProductPerPage, content);

            MessageBox.Show("Save success");
            
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (AppConfig.GetValue(AppConfig.NumberProductPerPage) != null)
            {
                nPhonePerPage = AppConfig.GetValue(AppConfig.NumberProductPerPage);
            }


            if (nPhonePerPage == "3")
                nProductComboBox.SelectedIndex = 0;
            else if (nPhonePerPage == "4")
                nProductComboBox.SelectedIndex = 1;
            else if (nPhonePerPage == "5")
                nProductComboBox.SelectedIndex = 2;
            else if (nPhonePerPage == "6")
                nProductComboBox.SelectedIndex = 3;
            else if (nPhonePerPage == "7")
                nProductComboBox.SelectedIndex = 4;
            else if (nPhonePerPage == "8")
                nProductComboBox.SelectedIndex = 5;
            else if (nPhonePerPage == "9")
                nProductComboBox.SelectedIndex = 6;
            else if (nPhonePerPage == "10")
                nProductComboBox.SelectedIndex = 7;
            else if (nPhonePerPage == "11")
                nProductComboBox.SelectedIndex = 8;
            else if (nPhonePerPage == "12")
                nProductComboBox.SelectedIndex = 9;

            if (AppConfig.GetValue("checkStartUp") == "true")
            {
                lastWindowCheckBox.IsChecked = true;
            }
            else { lastWindowCheckBox.IsChecked = false;}
        }

        private void lastWindowCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (lastWindowCheckBox.IsChecked == true)
            {
                isChecked = true;
                AppConfig.SetValue("checkStartUp", "true");
            }
            else
            {
                isChecked = false;
                AppConfig.SetValue("checkStartUp", "false");
            }
        }
    }
}
