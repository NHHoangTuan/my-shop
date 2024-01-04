using MyShop.BUS;
using MyShop.DTO;
using MyShop.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyShop.Views
{
    /// <summary>
    /// Interaction logic for ManageCategory.xaml
    /// </summary>
    public partial class ManageCategory : Page
    {

        CategoryBUS _catBUS;
        //BindingList<Category> _listCat;
        CategoryViewModel _catVM;
        
        

        public ManageCategory()
        {
            InitializeComponent();
            _catBUS = new CategoryBUS();
            _catVM = new CategoryViewModel();
            //_listCat = new BindingList<Category>();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            var screen = new AddCategory();
            screen.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            
            if (screen.ShowDialog() == true)
            {
                try
                {
                    var newCat = screen.newCat;
                    _catVM.catList.Add(newCat);
                    if (_catVM.catList.Count == 0)
                    {
                        MessageText.Text = "Opps! No brands found";
                    }
                    else
                    {
                        MessageText.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(screen, ex.Message);
                }
            }
        }

        private void UpdateCategory_Click(object sender, RoutedEventArgs e)
        {
            var p = (Category)catagoryListView.SelectedItem;
            if (p != null)
            {

                var screen = new UpdateCategory(p);
                screen.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                var result = screen.ShowDialog();
                if (result == true)
                {
                    var info = screen.updateCategory;
                    p.catName = info.catName;
                    p.description = info.description;
                    p.logoPath = info.logoPath;
                  

                    try
                    {
                        _catBUS.updateCategory(p);

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Exception here");
                        MessageBox.Show(screen, ex.Message);
                    }
                }
                else
                {

                }
            }

            else
            {
                MessageBox.Show("Please choose an item to update");
            }
        }

        private void RemoveCategory_Click(object sender, RoutedEventArgs e)
        {
            var p = (Category)catagoryListView.SelectedItem;
            if (p != null)
            {
                var result = MessageBox.Show($"You really want to remove category {p.catName} - ?",
               "Confirm remove", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _catBUS.deleteCategory(p);
                    _catVM.catList.Remove(p);
                    if (_catVM.catList.Count == 0)
                    {
                        MessageText.Text = "Opps! No brands found";
                    }
                    else
                    {
                        MessageText.Text = "";
                    }
                }
            }

            else
            {
                MessageBox.Show("Please choose an item to delete");
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _catVM.catList = _catBUS.getAllCategory();
            catagoryListView.ItemsSource = _catVM.catList;
            if (_catVM.catList.Count == 0)
            {
                MessageText.Text = "Opps! No brands found";
            }
            else
            {
                MessageText.Text = "";
            }
        }

        private void catagoryListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
