
using Aspose.Cells;
using Microsoft.Win32;
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
using System.Windows.Shell;

namespace MyShop.Views
{
    /// <summary>
    /// Interaction logic for ManageProducts.xaml
    /// </summary>
    public partial class ManageProducts : Page
    {

        PhoneBUS _phoneBUS;
        CategoryBUS _catBUS;
        PhoneViewModel _phoneVM;
        CategoryViewModel _catVM;
        List<Category>? _categoryList = null;

        private int _currentPage = 1;
        private int _rowsPerPage;
        private int _totalItems = 0;
        private int _totalPages = 0;
        private int _currentCurrency = 0;
        private int? _currentStartPrice = null;
        private int? _currentEndPrice = null;
        private string keyword = "";
        private int selectedCategoryIndex = -1;

        public ManageProducts()
        {
            InitializeComponent();
            _phoneBUS = new PhoneBUS();
            _phoneVM = new PhoneViewModel();
            _catVM = new CategoryViewModel();
            _catBUS = new CategoryBUS();
            _currentPage = 1;
            _currentStartPrice = null;
            _currentEndPrice = null;
            _currentCurrency = 0;
            _rowsPerPage = 6;
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _phoneVM.phoneList = _phoneBUS.getAllPhone();
            _phoneVM.selectedPhoneList = _phoneVM.phoneList.ToList();
            _categoryList = _catBUS.getAllCategory().ToList();

            categoriesListView.ItemsSource = _categoryList;

            foreach (var category in _categoryList)
            {
                category.phoneList = new BindingList<Phone>(_phoneBUS.getPhoneListByCategory(category.catID));
            }
            
            if (_categoryList.Count > 0)
            {
                categoriesListView.SelectedIndex = 0;
                selectedCategoryIndex = 0;
            }

            _rowsPerPage = _phoneBUS.getRowPerPage();


            updateDataList(_currentPage, _currentCurrency, _currentStartPrice, _currentEndPrice, _phoneVM.selectedPhoneList);

            
        }


        private void updateDataList(int page = 1, int currentCurrency = 0, int? currentStartPrice = null, int? currentEndPrice = null, List<Phone>? _currentList = null)
        {
           

            if (_currentList == null)
            {
                _currentList = _phoneVM.phoneList.ToList();
            }

           

            var listPhone = _currentList
               .Where((item) =>
               {

                   bool checkPrice = (item.promotionPrice >= currentStartPrice && item.promotionPrice <= currentEndPrice) ||
                   (currentStartPrice == null && currentEndPrice == null) || currentStartPrice == null || currentEndPrice == null;

                   return checkPrice;
               });

            _currentPage = page;

            _totalItems = listPhone.ToList().Count;
            _totalPages = listPhone.ToList().Count / _rowsPerPage +
                (listPhone.ToList().Count % _rowsPerPage == 0 ? 0 : 1);


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
                MessageText.Text = "Opps! No products found.";
            }
            else {
                MessageText.Text = "";
            }

            var showList = listPhone
                .Skip((_currentPage - 1) * _rowsPerPage)
                .Take(_rowsPerPage).ToList();

            phoneListView.ItemsSource = showList;

            if (currentCurrency != 0)
            {
                PriceCombobox.SelectedIndex = currentCurrency;
            }
            pageInfoTextBlock.Text = $"{_currentPage}/{_totalPages}";

        }


        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var phone = phoneListView.SelectedItem as Phone;
            if (phone == null) return;
            ProductDetail productDetail = new ProductDetail(phone);
            productDetail.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            if (productDetail.ShowDialog() == true)
            {
                if (productDetail.isUpdate == false)
                {
                    _phoneVM.phoneList = _phoneBUS.getAllPhone();
                    _phoneVM.selectedPhoneList.Clear();
                    _phoneVM.selectedPhoneList = _phoneVM.phoneList.ToList();
                    var selectedCategory = categoriesListView.Items[selectedCategoryIndex] as Category;
                    if (selectedCategory != null && selectedCategory.phoneList != null)
                    {
                        selectedCategory.phoneList.Remove(phone);
                        _phoneVM.selectedPhoneList = selectedCategory.phoneList.ToList();
                    }
                   
                    updateDataList(_currentPage, _currentCurrency, _currentStartPrice, _currentEndPrice, _phoneVM.selectedPhoneList);
                }
                else
                {

                }
                
            }
        }

        private void categoriesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            var selectedCategory = categoriesListView.SelectedItem as Category;
            if (selectedCategory == null) return;
            selectedCategoryIndex = categoriesListView.SelectedIndex;
            if (selectedCategory.phoneList == null) return;

            
            selectedCategory.phoneList = new BindingList<Phone>(_phoneBUS.getPhoneListByCategory(selectedCategory.catID));
            

            _phoneVM.selectedPhoneList = selectedCategory.phoneList.ToList();
            updateDataList(1, 0, null, null, _phoneVM.selectedPhoneList);
            SortCombobox.SelectedIndex = 0;
            PriceCombobox.SelectedIndex = 0;

        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var screen = new AddProduct(_categoryList!);
            screen.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            if (screen.ShowDialog() == true)
            {

                var catIndex = screen.catIndex;

                
                        try
                {
                    var newPhone = screen.newPhone;
                    _phoneVM.phoneList.Add(newPhone);
                    /*_phoneVM.selectedPhoneList.Clear();

                    _phoneVM.selectedPhoneList = _phoneVM.phoneList.ToList();*/
                    var selectedCategory = categoriesListView.Items[selectedCategoryIndex] as Category;
                    if (selectedCategory != null && selectedCategory.phoneList != null)
                    {
                        if (selectedCategory.catID == newPhone.category.catID)
                        {
                            selectedCategory.phoneList.Add(newPhone);
                            _phoneVM.selectedPhoneList = selectedCategory.phoneList.ToList();
                        }
                        
                    }

                    updateDataList(_currentPage, _currentCurrency, _currentStartPrice, _currentEndPrice, _phoneVM.selectedPhoneList);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(screen, ex.Message);
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
            updateDataList(1, _currentCurrency, _currentStartPrice, _currentEndPrice, _phoneVM.selectedPhoneList);
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            NextButton.IsEnabled = true;
            LastButton.IsEnabled = true;
            _currentPage--;

            updateDataList(_currentPage, _currentCurrency, _currentStartPrice, _currentEndPrice, _phoneVM.selectedPhoneList);
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

            updateDataList(_currentPage, _currentCurrency, _currentStartPrice, _currentEndPrice, _phoneVM.selectedPhoneList);
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
            updateDataList(_totalPages, _currentCurrency, _currentStartPrice, _currentEndPrice, _phoneVM.selectedPhoneList);
        }

        private void PriceCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PriceCombobox.SelectedIndex >= 0)
            {
                //Giá dưới 5 triệu
                if (PriceCombobox.SelectedIndex == 1)
                {
                    _currentStartPrice = 0;
                    _currentEndPrice = 5000000;
                    _currentCurrency = 1;
                    updateDataList(1, _currentCurrency, _currentStartPrice, _currentEndPrice, _phoneVM.selectedPhoneList);

                }
                // Giá từ 5 triệu đến 10 triệu
                if (PriceCombobox.SelectedIndex == 2)
                {
                    _currentStartPrice = 5000000;
                    _currentEndPrice = 10000000;
                    _currentCurrency = 2;
                    updateDataList(1, _currentCurrency, _currentStartPrice, _currentEndPrice, _phoneVM.selectedPhoneList);

                }
                // Giá dưới 10 triệu đến 15 triệu
                if (PriceCombobox.SelectedIndex == 3)
                {
                    _currentStartPrice = 10000000;
                    _currentEndPrice = 15000000;
                    _currentCurrency = 3;
                    updateDataList(1, _currentCurrency, _currentStartPrice, _currentEndPrice, _phoneVM.selectedPhoneList);

                }
                // Giá trên 15 triệu
                if (PriceCombobox.SelectedIndex == 4)
                {
                    _currentStartPrice = 15000000;
                    _currentEndPrice = int.MaxValue;
                    _currentCurrency = 4;
                    updateDataList(1, _currentCurrency, _currentStartPrice, _currentEndPrice, _phoneVM.selectedPhoneList);

                }

                if (PriceCombobox.SelectedIndex == 5)
                {
                    _currentStartPrice = null;
                    _currentEndPrice = null;
                    _currentCurrency = 5;
                    updateDataList(1, _currentCurrency, null, null, _phoneVM.selectedPhoneList);

                }
            }
        }

        private void SearchTermTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search_text = SearchTermTextBox.Text;
            if (search_text.Length > 0)
            {
                _currentPage = 1;
                PrevButton.IsEnabled = false;
                FirstButton.IsEnabled = false;

                var tempList = new List<Phone>();
                BindingList<Phone> phones = new BindingList<Phone>();
                foreach (Phone phone in _phoneVM.selectedPhoneList)
                {
                    if (phone.phoneName.ToLower().Contains(search_text.ToLower()))
                    {
                        phones.Add(phone);
                    }
                }

                tempList = phones
                .Skip((_currentPage - 1) * _rowsPerPage)
                .Take(_rowsPerPage).ToList();

                if (tempList.Count > 0)
                {
                    _currentPage = 1;
                    _totalItems = phones.Count;
                    _totalPages = phones.Count / _rowsPerPage +
                    (phones.Count % _rowsPerPage == 0 ? 0 : 1);
                    phoneListView.ItemsSource = tempList;
                    pageInfoTextBlock.Text = $"{_currentPage}/{_totalPages}";
                }
                if (_totalPages <= 1)
                {
                    NextButton.IsEnabled = false;
                    LastButton.IsEnabled = false;
                }
            }
            else
            {
                updateDataList(1,_currentCurrency,_currentStartPrice,_currentEndPrice,_phoneVM.selectedPhoneList);
            }
        }

        private void SortCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SortCombobox.SelectedIndex >= 0)
            {
                // Sort A-Z
                if (SortCombobox.SelectedIndex == 1)
                {

                    _phoneVM.selectedPhoneList = _phoneBUS.MergeSort(_phoneVM.selectedPhoneList, _phoneBUS.ComparePhonesByNameInCreasing);

                    updateDataList(1, _currentCurrency, _currentStartPrice, _currentEndPrice, _phoneVM.selectedPhoneList);

                }
                // Sort Z-A
                if (SortCombobox.SelectedIndex == 2)
                {

                    _phoneVM.selectedPhoneList = _phoneBUS.MergeSort(_phoneVM.selectedPhoneList, _phoneBUS.ComparePhonesByNameDeCreasing);

                    updateDataList(1, _currentCurrency, _currentStartPrice, _currentEndPrice, _phoneVM.selectedPhoneList);

                }

                if (SortCombobox.SelectedIndex == 3)
                {
                    _phoneVM.selectedPhoneList = _phoneBUS.MergeSort(_phoneVM.selectedPhoneList, _phoneBUS.ComparePhonesByPriceDecreasing);

                    updateDataList(1, _currentCurrency, _currentStartPrice, _currentEndPrice, _phoneVM.selectedPhoneList);
                }

                if (SortCombobox.SelectedIndex == 4)
                {
                    _phoneVM.selectedPhoneList = _phoneBUS.MergeSort(_phoneVM.selectedPhoneList, _phoneBUS.ComparePhonesByPriceIncreasing);

                    updateDataList(1, _currentCurrency, _currentStartPrice, _currentEndPrice, _phoneVM.selectedPhoneList);
                }

                if (SortCombobox.SelectedIndex == 5)
                {
                    _phoneVM.selectedPhoneList.Clear();
                    var selectedCategory = categoriesListView.Items[selectedCategoryIndex] as Category;
                    if (selectedCategory != null && selectedCategory.phoneList != null)
                    {
                        _phoneVM.selectedPhoneList = selectedCategory.phoneList.ToList();
                    }
                    
                    updateDataList(1, _currentCurrency, null, null, _phoneVM.selectedPhoneList);

                }
            }
        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            _categoryList = new List<Category>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var screen = new OpenFileDialog();

            if (screen.ShowDialog() == true)
            {
                string filename = screen.FileName;
                //_categoryList = _catBUS.getCategoryListFromExcel(filename);
                _phoneBUS.getPhoneListFromExcel(filename);
               
                Page_Loaded(sender, e);
                
                /*categoriesListView.ItemsSource = _categoryList;
                updateDataList(1, _currentCurrency, _currentStartPrice, _currentEndPrice, _phoneVM.selectedPhoneList );*/
            }
        }
    }
}
