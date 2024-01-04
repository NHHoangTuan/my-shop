using MyShop.BUS;
using MyShop.DAO;
using MyShop.DTO;
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
    /// Interaction logic for AdvancedStatistic.xaml
    /// </summary>
    public partial class AdvancedStatistic : Page
    {
        private static int nextSTT = 1;

        // Mục đích là đổ dữ liệu của Class này lên UI
        class Data :INotifyPropertyChanged
        {
            
            public string phoneName {  get; set; }
            public string imagePath { get; set; }
            public string manufacturer {  get; set; }
            public int stt { get; set; }
            public int totalSales { get; set; }

            public Data(Phone phone, int totalSales)
            {
                phoneName = phone.phoneName;
                imagePath = phone.imagePath;
                manufacturer = phone.manufacturer;
                stt = nextSTT;
                this.totalSales = totalSales;
            }

            public event PropertyChangedEventHandler? PropertyChanged;
        }

        private Frame _pageNavigation;
        private List<Phone> top5Phones;
        private List<int> top5Quantity;
        private ReportBUS _reportBUS;
        //private List<Data> _listShow;

        public AdvancedStatistic(Frame pageNavigation)
        {
            InitializeComponent();
            _pageNavigation = pageNavigation;
            top5Phones = new List<Phone>();
            top5Quantity = new List<int>();
            _reportBUS = new ReportBUS();
            //_listShow = new List<Data>();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ChooseCombobox.SelectedIndex = 0;
            nextSTT = 1;
            showReportInYear();
            
        }

        private void showReportInYear()
        {
            List<Data> list = new List<Data>();
            (top5Phones, top5Quantity) = _reportBUS.top5SalesByYear(DateTime.Now.Year);
            
            for (int i = 0; i < top5Quantity.Count; i++)
            {
                list.Add(new Data(top5Phones[i], top5Quantity[i]));
                nextSTT++;
            }
            top5ListView.ItemsSource = list;
        }


        private void showReportInMonth()
        {
            List <Data> list = new List<Data>();
            (top5Phones, top5Quantity) = _reportBUS.top5SalesByMonth(DateTime.Now.Year, DateTime.Now.Month);
            
            for (int i = 0; i < top5Quantity.Count; i++)
            {
                list.Add(new Data(top5Phones[i], top5Quantity[i]));
                nextSTT++;
            }
            top5ListView.ItemsSource = list;
        }

        private void showReportInWeek()
        {
            List<Data> list = new List<Data>();
            (top5Phones, top5Quantity) = _reportBUS.top5SalesByWeek(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            for (int i = 0; i < top5Quantity.Count; i++)
            {
                list.Add(new Data(top5Phones[i], top5Quantity[i]));
                nextSTT++;
            }
            top5ListView.ItemsSource = list;
        }


        private void top5ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ChooseCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ChooseCombobox.SelectedIndex == 0)
            {
                nextSTT = 1;
                showReportInYear();
                
            }
            
            if (ChooseCombobox.SelectedIndex == 1)
            {
                nextSTT = 1;
                showReportInMonth();
                
            }

            if ((ChooseCombobox.SelectedIndex == 2))
            {

                nextSTT = 1;
                showReportInWeek();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _pageNavigation.NavigationService.GoBack();
        }

        
    }
}
