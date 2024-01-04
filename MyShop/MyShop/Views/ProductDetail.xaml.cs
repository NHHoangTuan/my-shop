using MyShop.BUS;
using MyShop.DAO;
using MyShop.DTO;
using MyShop.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyShop.Views
{
    /// <summary>
    /// Interaction logic for ProductDetail.xaml
    /// </summary>
    public partial class ProductDetail : Window
    {

        Phone selectedPhone;
        PhoneBUS _phoneBUS;
        PhoneViewModel _phoneVM;
        public bool isUpdate = false;
        PromotionViewModel _promotionVM;
        PromotionBUS _promotionBUS;
        int flag = 0;
        Promotion _currentPromo;
        

        public ProductDetail(Phone phone)
        {
            InitializeComponent();
            _phoneBUS = new PhoneBUS();
            _phoneVM = new PhoneViewModel();
            _promotionVM = new PromotionViewModel();
            _promotionBUS = new PromotionBUS();
            _currentPromo = new Promotion();
            this.DataContext = phone;
            selectedPhone = phone;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        
        private void PromotionsCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var promotion = (Promotion)PromotionsCombobox.SelectedValue;

            if (promotion != null && flag != 0)
            {
                
                Promotion selectedPromo = (Promotion)promotion.Clone();
                selectedPhone.promotion = selectedPromo;

                
                double percent = 1 - promotion.discountPercent * 1.0 / 100;
                selectedPhone.promotionPrice = (decimal?)((double)selectedPhone.price * percent);
                _phoneBUS.updatePhone(selectedPhone);
                
                MessageBox.Show("Apply promotion success", "Notice");
                _currentPromo = selectedPromo;
            }
            flag = 1;
        }

        private void DelProduct_Click(object sender, RoutedEventArgs e)
        {
            
            if (selectedPhone != null)
            {
                var result = MessageBox.Show($"You really want to remove {selectedPhone.phoneName} - {selectedPhone.manufacturer}?",
               "Confirm remove", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _phoneBUS.deletePhone(selectedPhone);
                    isUpdate = false;
                    DialogResult = true;

                    //updateDataSource(_currentPage, _currentCurrency, _currentStartPrice, _currentEndPrice, _currentList);
                }
            }

            else
            {
                MessageBox.Show("Please choose an item to delete");
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (selectedPhone != null)
            {

                var screen = new UpdateProduct(selectedPhone);
                screen.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                
                if (screen.ShowDialog() == true)
                {
                    var info = screen.updatePhone;
                    selectedPhone.phoneName = info.phoneName;
                    selectedPhone.manufacturer = info.manufacturer;
                    selectedPhone.price = info.price;
                    selectedPhone.imagePath = info.imagePath;
                    selectedPhone.batteryCapacity = info.batteryCapacity;
                    selectedPhone.phoneRam = info.phoneRam;
                    selectedPhone.phoneRom = info.phoneRom;
                    selectedPhone.screenSize = info.screenSize;
                    selectedPhone.quantity = info.quantity;
                    selectedPhone.category = info.category;
                    //updateDataSource(_currentPage, _currentCurrency, _currentStartPrice, _currentEndPrice, _currentList);

                    try
                    {
                        _phoneBUS.updatePhone(selectedPhone);
                        isUpdate = true;
                        //DialogResult = true;

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

            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _promotionVM.promotions = _promotionBUS.getAllPromotion();
            _promotionVM.promotions.Insert(0, new Promotion()
            {
                promotionCode = "No Apply"
            });
            PromotionsCombobox.ItemsSource = _promotionVM.promotions;


            // neu co ap dung khuyen mai
            if (selectedPhone.promotion != null)
            {
                PromotionsCombobox.SelectedValue = (Promotion)_promotionVM.promotions.Where(item => item.promotionID == selectedPhone.promotion.promotionID).ToList()[0];
            }
            else // neu khong ap dung khuyen mai
            {
                PromotionsCombobox.SelectedIndex = 0;
                selectedPhone.promotion = null;
            }
            
        }
    }
}
