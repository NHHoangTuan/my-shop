using MyShop.BUS;
using MyShop.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Window
    {

        BindingList<Phone> listPhone;
        PhoneBUS _phoneBUS;
        Phone selectedPhone;
        PurchaseBUS _purchaseBUS;
        OrderBUS _orderBUS;
        private int selectedIndex = -1;
        private int _currentQuantity = 0;
        private BindingList<Purchase> listPurchase;
        private Purchase _currentPurchase;
        private Decimal finalTotal = 0;
        private Decimal profitTotal = 0;
        public Order newOrder {  get; set; }

        public AddOrder()
        {
            InitializeComponent();
            listPhone = new BindingList<Phone>();
            _purchaseBUS = new PurchaseBUS();
            _orderBUS = new OrderBUS();
            listPurchase = new BindingList<Purchase>();
            _phoneBUS = new PhoneBUS();
            selectedPhone = new Phone();
            _currentPurchase = new Purchase();
            newOrder = new Order();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listPhone = _phoneBUS.getAllPhone();
            ProductCombobox.ItemsSource = listPhone;

            selectedPhone = listPhone[0];
            selectedIndex = 0;
            ProductCombobox.SelectedIndex = 0;
            this.DataContext = selectedPhone;
            ordersListView.ItemsSource = listPurchase;
        }


        private void ProductCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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

            listPurchase.Add(newPurchase);
            QuantityTermTextBox.Text = "";

            //loi nhuan tren moi purchase
            var profitPerPurchase = (_orderBUS.calProductProfit(newPurchase.phone.price) - newPurchase.phone.price) * newPurchase.quantity;

            finalTotal += newPurchase.totalPrice;
            profitTotal += profitPerPurchase;
           

            FinalPrice.Text = finalTotal.ToString();

        }

        

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var result = MessageBox.Show("Confirm remove?", "Notice", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                var selectedPurchase = ordersListView.SelectedItem as Purchase;
                if (selectedPurchase == null) { return; }
                listPurchase.Remove(selectedPurchase);
                finalTotal -= selectedPurchase.totalPrice;
                var profitPerPurchase = (_orderBUS.calProductProfit(selectedPurchase.phone.price) - selectedPurchase.phone.price) * selectedPurchase.quantity;
                profitTotal -= profitPerPurchase;

                FinalPrice.Text = finalTotal.ToString();
            }
            
        }

        private void SaveOrder_Click(object sender, RoutedEventArgs e)
        {

            if (CustomerTermTextBox.Text == "")
            {
                MessageBox.Show("Please enter customer name", "Notice", MessageBoxButton.OK);
                return;
            }

            if (AddressTermTextBox.Text == "")
            {
                MessageBox.Show("Please enter customer address", "Notice", MessageBoxButton.OK);
                return;
            }


            newOrder.cusName = CustomerTermTextBox.Text.ToString();
            newOrder.cusAddress = AddressTermTextBox.Text.ToString();
            newOrder.orderDate = DateTime.Now.Date;
            newOrder.priceTotal = finalTotal;
            newOrder.profitTotal = profitTotal;
            
            // lay id, dong thoi insert vao database
            int _currentOrderID = _orderBUS.saveOrder(newOrder);
            newOrder.orderID = _currentOrderID;

            foreach (var purchase in listPurchase)
            {
                purchase.orderID = _currentOrderID;
            }
            // lúc này mới lưu vào database 
            foreach (var purchase in listPurchase)
            {

                // lúc mà thêm purchase thì ở DAO đã xóa số lượng bên product luôn rồi !
                _purchaseBUS.savePurchase(purchase);
            }

            

            MessageBox.Show("Save order success", "Notice");
            DialogResult = true;

        }

        
    }
}
