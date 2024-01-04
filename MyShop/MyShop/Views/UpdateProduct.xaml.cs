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
    /// Interaction logic for UpdateProduct.xaml
    /// </summary>
    public partial class UpdateProduct : Window
    {

        public FileInfo? _selectedImage = null;
        private bool _isImageChanged = false;
        private PhoneBUS _phoneBUS;
        public Phone updatePhone { get; set; }
        private CategoryBUS _catBUS;
        

        public UpdateProduct(Phone info)
        {
            InitializeComponent();
            _phoneBUS = new PhoneBUS();
            _catBUS = new CategoryBUS();
            
            updatePhone = (Phone)info.Clone();
            
            var categories = _catBUS.getAllCategory();
            foreach (var category in categories)
            {
                if (category.catID == updatePhone.category.catID)
                {
                    updatePhone.category = category;
                }
            }

            CategoryCombobox.ItemsSource = categories;
            this.DataContext = updatePhone;

            CategoryCombobox.SelectedItem = updatePhone.category;
        }

        private void AddImageButton_Click(object sender, RoutedEventArgs e)
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

                UpdatedImage.Source = bitmap;

            }
        }

        private void SaveProduct_Click(object sender, RoutedEventArgs e)
        {
            if (_isImageChanged)
            {

                int id = updatePhone.phoneID;
                string key = Guid.NewGuid().ToString();
                updatePhone.imagePath = _phoneBUS.uploadImage(_selectedImage, id, key);
            }


            MessageBox.Show("Update phone success", "Notice", MessageBoxButton.OK);
            DialogResult = true;
        }
    }
}
