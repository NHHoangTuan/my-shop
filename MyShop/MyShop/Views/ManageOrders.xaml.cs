using MyShop.BUS;
using MyShop.DTO;
using MyShop.ViewModel;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyShop.Views
{
    /// <summary>
    /// Interaction logic for ManageOrders.xaml
    /// </summary>
    public partial class ManageOrders : Page
    {


        OrderBUS _orderBUS;

        OrderViewModel _orderVM;


        private int _currentPage = 1;
        private int _rowsPerPage;
        private int _totalItems = 0;
        private int _totalPages = 0;
        private DateTime? _currentStartDate = null;
        private DateTime? _currentEndDate = null;
        private int selectedCategoryIndex = -1;
        public ManageOrders()
        {
            InitializeComponent();
            
            _orderVM = new OrderViewModel();
            _orderBUS = new OrderBUS();
            _currentPage = 1;
            _currentStartDate = null;
            _currentEndDate = null;
            _rowsPerPage = 9;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
         
            _orderVM.orderList = _orderBUS.getAllOrder();

            _orderVM.selectedOrderList = _orderVM.orderList.ToList();
            

            updateDataList(_currentPage, _currentStartDate, _currentEndDate, _orderVM.selectedOrderList);
        }


        private void updateDataList(int page = 1, DateTime? currentStartDate = null, DateTime? currentEndDate = null, List<Order>? _currentList = null)
        {


            if (_currentList == null)
            {
                _currentList = _orderVM.orderList.ToList();
            }

            var listOrder = _currentList
                .Where((item) =>
                {
                    bool checkStartDate = currentStartDate == null || item.orderDate >= currentStartDate;
                    bool checkEndDate = currentEndDate == null || item.orderDate <= currentEndDate;
                    return checkStartDate && checkEndDate;
                });


            _currentPage = page;
            /*if (listOrder.Count() > 0)
            {
                _currentPage = 1;
            }*/



            _totalItems = listOrder.ToList().Count;
            _totalPages = listOrder.ToList().Count / _rowsPerPage +
                (listOrder.ToList().Count % _rowsPerPage == 0 ? 0 : 1);


            if (_currentPage >= _totalPages)
            {
                _currentPage = _totalPages;
                LastButton.IsEnabled = false;
                NextButton.IsEnabled = false;
            }
            else
            {
                LastButton.IsEnabled = true;
                NextButton.IsEnabled = true;
            }

            if (_currentPage == 1)
            {
                FirstButton.IsEnabled = false;
                PrevButton.IsEnabled = false;
            }

            if (_currentPage == 1)
            {
                if (_totalPages > 1)
                {
                    NextButton.IsEnabled = true;
                    LastButton.IsEnabled = true;
                }
                else
                {
                    NextButton.IsEnabled = false;
                    LastButton.IsEnabled = false;
                }
            }


            if (_currentList.Count == 0)
            {
                MessageText.Text = "Opps! No order found.";
            }
            else
            {
                MessageText.Text = "";
            }

            var showList = listOrder
                .Skip((_currentPage - 1) * _rowsPerPage)
                .Take(_rowsPerPage).ToList();

            ordersListView.ItemsSource = showList;

            
            pageInfoTextBlock.Text = $"{_currentPage}/{_totalPages}";

        }


        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            _currentStartDate = dateStartPicker.SelectedDate;
            _currentEndDate = dateEndPicker.SelectedDate;
            updateDataList(1, _currentStartDate, _currentEndDate, _orderVM.selectedOrderList);
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            var addOrder = new AddOrder();
            addOrder.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            if (addOrder.ShowDialog() == true)
            {
                try
                {
                    var newOrder = addOrder.newOrder;
                    _orderVM.orderList.Add(newOrder);
                    _orderVM.selectedOrderList.Clear();
                    _orderVM.selectedOrderList = _orderVM.orderList.ToList();

                    updateDataList(_currentPage, _currentStartDate, _currentEndDate, _orderVM.selectedOrderList);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(addOrder, ex.Message);
                }
            }
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var order = ordersListView.SelectedItem as Order;
            if (order == null) return;
            OrderDetail orderDetail = new OrderDetail(order);
            orderDetail.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            if (orderDetail.ShowDialog() == true)
            {
                if (orderDetail.isUpdate == false)
                {

                    _orderVM.orderList = _orderBUS.getAllOrder();
                    _orderVM.selectedOrderList.Clear();
                    _orderVM.selectedOrderList = _orderVM.orderList.ToList();

                   

                    updateDataList(_currentPage, _currentStartDate, _currentEndDate, _orderVM.selectedOrderList);
                }
                else
                {

                }

            }
        }

        

        private void FirstButton_Click(object sender, RoutedEventArgs e)
        {
            if (_totalPages <= 1)
            {
                NextButton.IsEnabled = false;
                LastButton.IsEnabled = false;
            }
            updateDataList(1, _currentStartDate, _currentEndDate, _orderVM.selectedOrderList);
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            NextButton.IsEnabled = true;
            LastButton.IsEnabled = true;
            _currentPage--;

            updateDataList(_currentPage, _currentStartDate, _currentEndDate, _orderVM.selectedOrderList);
            if (_currentPage - 1 < 1)
            {
                PrevButton.IsEnabled = false;
                FirstButton.IsEnabled = false;
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            PrevButton.IsEnabled = true;
            FirstButton.IsEnabled = true;
            _currentPage++;

            updateDataList(_currentPage, _currentStartDate, _currentEndDate, _orderVM.selectedOrderList);
            if (_currentPage + 1 > _totalPages)
            {
                NextButton.IsEnabled = false;
                LastButton.IsEnabled = false;
            }
        }

        private void LastButton_Click(object sender, RoutedEventArgs e)
        {
            if (_totalPages > 1)
            {
                FirstButton.IsEnabled = true;
                PrevButton.IsEnabled = true;
            }
            updateDataList(_totalPages, _currentStartDate, _currentEndDate, _orderVM.selectedOrderList);
        }

       
    }
}
