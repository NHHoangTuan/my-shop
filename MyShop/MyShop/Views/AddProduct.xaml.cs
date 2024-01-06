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
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {

        private FileInfo? _selectedImage = null;
        private PhoneBUS _phoneBUS;
        private bool _isImageChanged = false;
        public Phone newPhone { get; set; }
        public int catIndex = -1;

        public AddProduct(List<Category> categories)
        {
            InitializeComponent();
            _phoneBUS = new PhoneBUS();
            newPhone = new Phone();
            this.DataContext = newPhone;
            CategoryCombobox.ItemsSource = categories;
        }

        private void SaveProduct_Click(object sender, RoutedEventArgs e)
        {
            var category = (Category)CategoryCombobox.SelectedItem;

            if (_selectedImage == null)
            {
                MessageBox.Show("Please enter image");
                return;
            }
            if (category == null)
            {
                MessageBox.Show(this, "Invalid category");
                return;
            }
            newPhone.category = category;
            int id = _phoneBUS.saveProduct(newPhone);
            string key = Guid.NewGuid().ToString();
            newPhone.imagePath = _phoneBUS.uploadImage(_selectedImage, id, key);
            newPhone.phoneID = id;
            newPhone.promotionPrice = newPhone.price;
            newPhone.promotion = new Promotion()
            {
                promotionID = null,
                discountPercent = 0,
                promotionCode = "No Apply"
            };
            MessageBox.Show("Add phone success", "Notice", MessageBoxButton.OK);
            DialogResult = true;
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

                AddedImage.Source = bitmap;

            }
        }
    }
}
