using Microsoft.Win32;
using MyShop.BUS;
using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for AddCategory.xaml
    /// </summary>
    public partial class AddCategory : Window
    {

        private FileInfo? _selectedImage = null;
        private CategoryBUS _catBUS;
        private bool _isImageChanged = false;
        public Category newCat {  get; set; }

        public AddCategory()
        {
            InitializeComponent();
            _catBUS = new CategoryBUS();
            newCat = new Category();
            this.DataContext = newCat;
        }

        private void AddLogoImageButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new OpenFileDialog();
            screen.Filter = "Files|*.png; *.jpg; *.jpeg;";
            if (screen.ShowDialog() == true)
            {
                _isImageChanged = true;
                _selectedImage = new FileInfo(screen.FileName);

                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(screen.FileName, UriKind.Absolute);
                bitmap.EndInit();

                AddedImage.Source = bitmap;

            }
        }

        private void SaveCat_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedImage == null)
            {
                MessageBox.Show("Please enter image");
                return;
            }

            int id = _catBUS.saveCategory(newCat);
            string key = Guid.NewGuid().ToString();
            newCat.logoPath = _catBUS.uploadImage(_selectedImage, id, key);
            newCat.catID = id;
            MessageBox.Show("Add category success", "Notice", MessageBoxButton.OK);
            DialogResult = true;
        }
    }
}
