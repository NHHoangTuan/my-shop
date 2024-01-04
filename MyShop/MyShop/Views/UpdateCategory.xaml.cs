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
    /// Interaction logic for UpdateCategory.xaml
    /// </summary>
    public partial class UpdateCategory : Window
    {

        public FileInfo? _selectedImage = null;
        private bool _isImageChanged = false;
        private CategoryBUS _catBUS;
        public Category updateCategory { get; set; }

        public UpdateCategory(Category info)
        {
            InitializeComponent();
            _catBUS = new CategoryBUS();
            updateCategory = (Category)info.Clone();
            this.DataContext = updateCategory;
        }

        private void EditLogoImageButton_Click(object sender, RoutedEventArgs e)
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

               UpdateImage.Source = bitmap;

            }
        }

        private void SaveCat_Click(object sender, RoutedEventArgs e)
        {
            if (_isImageChanged)
            {

                int id = updateCategory.catID;
                string key = Guid.NewGuid().ToString();
                updateCategory.logoPath = _catBUS.uploadImage(_selectedImage, id, key);
            }


            MessageBox.Show("Update phone success", "Notice", MessageBoxButton.OK);
            DialogResult = true;
        }

        
    }
}
