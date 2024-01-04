using MyShop.BUS;
using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for UpdateOrder.xaml
    /// </summary>
    public partial class UpdateOrder : Window
    {

        private OrderBUS _orderBUS;
        private PhoneBUS _phoneBUS;
        public Order updateOrder { get; set; }
        private PurchaseBUS _purchaseBUS;
        private BindingList<Purchase> _listPurchase;
        int _currentQuantity = 0;
        Phone selectedPhone;
        private BindingList<Phone> _listPhone;
        private int selectedIndex = 0;
        private Decimal profitTotal = 0;
        private Decimal finalTotal = 0;

        public UpdateOrder(Order info)
        {
            InitializeComponent();
            _orderBUS = new OrderBUS();
            _purchaseBUS = new PurchaseBUS();
            _phoneBUS = new PhoneBUS();

            updateOrder = (Order)info.Clone();
            selectedPhone = new Phone();
            
            _listPhone = new BindingList<Phone>();
            _listPurchase = new BindingList<Purchase>();

            
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _listPurchase = _purchaseBUS.getAllPurchaseByOrderID(updateOrder.orderID);
            ordersListView.ItemsSource = _listPurchase;
            _listPhone = _phoneBUS.getAllPhone();
            ProductCombobox.ItemsSource = _listPhone;

            selectedPhone = _listPhone[0];
            selectedIndex = 0;
            ProductCombobox.SelectedIndex = 0;
            this.DataContext = selectedPhone;

            foreach (var purchase in _listPurchase)
            {
                var profitPerPurchase = (_orderBUS.calProductProfit(purchase.phone.price) - purchase.phone.price) * purchase.quantity;

                finalTotal += purchase.totalPrice;
                profitTotal += profitPerPurchase;
            }

            CustomerTermTextBox.Text = updateOrder.cusName;
            AddressTermTextBox.Text = updateOrder.cusAddress;
            FinalPrice.Text = finalTotal.ToString();
            
        }

        private void ProductCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*selectedIndex = ProductCombobox.SelectedIndex;
            if (selectedIndex < 0) { return; }
            selectedPhone.copy(_listPhone[selectedIndex]);*/
            selectedIndex = (int)ProductCombobox.SelectedIndex;
            selectedPhone = ProductCombobox.Items[selectedIndex] as Phone;
            this.DataContext = selectedPhone;
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (QuantityTermTextBox.Text == "")
            {
                MessageBox.Show("Please enter quantity", "Notice", MessageBoxButton.OK);
                return;
            }

            int quantity = int.Parse(QuantityTermTextBox.Text);
            _currentQuantity = quantity;
            if (_currentQuantity > selectedPhone.quantity)
            {
                _currentQuantity = selectedPhone.quantity;
            }

            var newPhone = new Phone();
            newPhone = (Phone)selectedPhone.Clone();
            // add purchase
            var newPurchase = new Purchase()
            {
                phone = newPhone,
                quantity = _currentQuantity,
                totalPrice = _currentQuantity * (decimal)selectedPhone.promotionPrice
            };

            _listPurchase.Add(newPurchase);
            QuantityTermTextBox.Text = "";

            // loi nhuan tren moi purchase
            var profitPerPurchase = (_orderBUS.calProductProfit(newPurchase.phone.price) - newPurchase.phone.price) * newPurchase.quantity;
            
            finalTotal += newPurchase.totalPrice;
            profitTotal += profitPerPurchase;
            

            FinalPrice.Text = finalTotal.ToString();
        }

        

        private void SaveOrder_Click(object sender, RoutedEventArgs e)
        {
            updateOrder.cusName = CustomerTermTextBox.Text.ToString();
            updateOrder.cusAddress = AddressTermTextBox.Text.ToString();
            updateOrder.priceTotal = finalTotal;
            updateOrder.profitTotal = profitTotal;

            foreach (var purchase in _listPurchase)
            {
                purchase.orderID = updateOrder.orderID;
                if (purchase.purchaseID == 0) { _purchaseBUS.savePurchase(purchase); }
                else { _purchaseBUS.updatePurchase(purchase); }
                
            }

            MessageBox.Show("Update phone success", "Notice", MessageBoxButton.OK);
            DialogResult = true;
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var result = MessageBox.Show("Confirm remove?", "Notice", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                var selectedPurchase = ordersListView.SelectedItem as Purchase;
                if (selectedPurchase == null) { return; }
                _listPurchase.Remove(selectedPurchase);
                _purchaseBUS.deletePurchase(selectedPurchase);
                finalTotal -= selectedPurchase.totalPrice;
                var profitPerPurchase = (_orderBUS.calProductProfit(selectedPurchase.phone.price) - selectedPurchase.phone.price) * selectedPurchase.quantity;
                profitTotal -= profitPerPurchase;

                FinalPrice.Text = finalTotal.ToString();
            }
        }

       

        
    }
}
