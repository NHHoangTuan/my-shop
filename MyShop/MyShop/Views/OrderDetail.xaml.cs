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
using System.Windows.Shapes;

namespace MyShop.Views
{
    /// <summary>
    /// Interaction logic for OrderDetail.xaml
    /// </summary>
    public partial class OrderDetail : Window
    {
        Order selectedOrder;
        OrderBUS _orderBUS;
        OrderViewModel _orderVM;
        public bool isUpdate = false;
        BindingList<Purchase> listPurchase;
        PurchaseBUS _purchaseBUS;

        public OrderDetail(Order order)
        {
            InitializeComponent();
            _orderBUS = new OrderBUS();
            _orderVM = new OrderViewModel();
            listPurchase = new BindingList<Purchase>();
            _purchaseBUS = new PurchaseBUS();
            this.DataContext = order;
            selectedOrder = order;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listPurchase = _purchaseBUS.getAllPurchaseByOrderID(selectedOrder.orderID);
            selectedOrder.priceTotal = 0;
            selectedOrder.profitTotal = 0;
            foreach (Purchase purchase in listPurchase)
            {
                var profitPerPurchase = (_orderBUS.calProductProfit(purchase.phone.price) - purchase.phone.price) * purchase.quantity;

                selectedOrder.priceTotal += purchase.totalPrice;
                selectedOrder.profitTotal += profitPerPurchase;
                _orderBUS.updateOrder(selectedOrder);
            }
            productsListView.ItemsSource = listPurchase;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void UpdateOrder_Click(object sender, RoutedEventArgs e)
        {
            if (selectedOrder != null)
            {

                var screen = new UpdateOrder(selectedOrder);
                screen.WindowStartupLocation = WindowStartupLocation.CenterScreen;

                if (screen.ShowDialog() == true)
                {
                    var info = screen.updateOrder;
                    selectedOrder.cusName = info.cusName;
                    selectedOrder.cusAddress = info.cusAddress;
                    selectedOrder.profitTotal = info.profitTotal;
                    selectedOrder.priceTotal = info.priceTotal;
                    selectedOrder.orderDate = info.orderDate;

                    //updateDataSource(_currentPage, _currentCurrency, _currentStartPrice, _currentEndPrice, _currentList);

                    try
                    {
                        _orderBUS.updateOrder(selectedOrder);

                        isUpdate = true;
                        Window_Loaded(sender, e);
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

        private void DelOrder_Click(object sender, RoutedEventArgs e)
        {
            if (selectedOrder != null)
            {
                var result = MessageBox.Show($"You really want to remove?",
               "Confirm remove", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _orderBUS.deleteOrder(selectedOrder);
                    _purchaseBUS.deletePurchaseByOrderID(selectedOrder.orderID);
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

        
    }
}
