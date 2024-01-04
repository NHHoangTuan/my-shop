using LiveCharts.Wpf;
using LiveCharts;
using MyShop.BUS;
using System;
using System.Collections.Generic;
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
using MyShop.DTO;

namespace MyShop.Views
{
    /// <summary>
    /// Interaction logic for ProductStatistic.xaml
    /// </summary>
    public partial class ProductStatistic : Page
    {

        private Frame _pageNavigation;
        private ReportBUS _reportBUS;
        private CartesianChart _chart;
        private Phone? _selectedPhone;
        private List<Phone> _listAllPhone;
        private PhoneBUS _phoneBUS;
        private int _selectedIndex = 0;
        private int _currentYear;
        private int startYear = 2023;
        private int endYear = 2026;

        public ProductStatistic(Frame pageNavigation)
        {
            InitializeComponent();
            _pageNavigation = pageNavigation;
            _chart = chart;
            _reportBUS = new ReportBUS();
            _selectedPhone = new Phone();
            _listAllPhone = new List<Phone>();
            _phoneBUS = new PhoneBUS();
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _listAllPhone = _phoneBUS.getAllPhone().ToList();
            PhoneCombobox.ItemsSource = _listAllPhone;

            PhoneCombobox.SelectedIndex = _selectedIndex;
            _selectedPhone = PhoneCombobox.Items[_selectedIndex] as Phone;

            if (_selectedPhone != null)
            {
                displayYearMode(_selectedPhone);
            }
            
        }


        private void displayYearMode(Phone phone)
        {
            var quantityByYear = _reportBUS.groupQuantityOfProductByYear(phone, startYear, endYear);

            //var valuesLineChart = new ChartValues<double>();
            
            var valuesColChart = new ChartValues<double>();

            foreach (var item in quantityByYear)
            {
                valuesColChart.Add((double)item);
            }

            

            _chart.Series = new SeriesCollection();
            _chart.AxisX = new AxesCollection();

            _chart.Series.Add(new ColumnSeries()
            {
                Fill = Brushes.Chocolate,
                Title = "Sales by year",
                Values = valuesColChart
            });

            chart.AxisX.Add(
                new Axis()
                {
                    Foreground = Brushes.Black,
                    Title = "Year",
                    Labels = new string[] {
                        "2023",
                        "2024",
                        "2025",
                        "2026"
                    }
                });
            Title.Text = "Showing sales by year";
        }

        private void displayMonthMode(Phone phone, int year)
        {

            var quantityByMonth = _reportBUS.groupQuantityOfProductByMonth(phone, year);


            var valuesColChart = new ChartValues<double>();

            foreach (var item in quantityByMonth)
            {
                valuesColChart.Add((double)item);
            }

          

            _chart.Series = new SeriesCollection();
            _chart.AxisX = new AxesCollection();

            

            _chart.Series.Add(new ColumnSeries()
            {
                Fill = Brushes.Chocolate,
                Title = "Sales by month",
                Values = valuesColChart
            });

            chart.AxisX.Add(
                new Axis()
                {
                    Foreground = Brushes.Black,
                    Title = "Month",
                    Labels = new string[] {
                        "January",
                        "February",
                        "March",
                        "April",
                        "May",
                        "June",
                        "July",
                        "August",
                        "September",
                        "October",
                        "November",
                        "December",
                    }
                });
            Title.Text = "Showing sales by month";
            MonthCombobox.IsEnabled = true;
            MonthCombobox.SelectedIndex = 0;
        }


        private void displayWeekMode(Phone phone, int month, int year)
        {

            var quantityByWeek = _reportBUS.groupQuantityOfProductByWeek(phone, year, month);

            var valuesCol1Chart = new ChartValues<double>();
            var valuesColChart = new ChartValues<double>();

            foreach (var item in quantityByWeek)
            {
                valuesColChart.Add((double)item);
            }

            

            _chart.Series = new SeriesCollection();
            _chart.AxisX = new AxesCollection();

            

            _chart.Series.Add(new ColumnSeries()
            {
                Fill = Brushes.Chocolate,
                Title = "Sales by week",
                Values = valuesColChart
            });

            chart.AxisX.Add(
                new Axis()
                {
                    Foreground = Brushes.Black,
                    Title = "Week",
                    Labels = new string[] {
                        "Week 1",
                        "Week 2",
                        "Week 3",
                        "Week 4",
                        "Week 5",
                    }
                });
            Title.Text = "Showing sales by week";
        }


        private void displayDateMode(Phone phone, DateTime startDate, DateTime endDate)
        {

            var quantityByDate = _reportBUS.groupQuantityOfProductByDate(phone, startDate, endDate);

   
            var valuesColChart = new ChartValues<double>();

            foreach (var item in quantityByDate)
            {
                valuesColChart.Add((double)item);
            }
            

            _chart.Series = new SeriesCollection();
            _chart.AxisX = new AxesCollection();

           

            _chart.Series.Add(new ColumnSeries()
            {
                Fill = Brushes.Chocolate,
                Title = "Sales by day",
                Values = valuesColChart
            });

            chart.AxisX.Add(
                new Axis()
                {
                    Foreground = Brushes.Black,
                    Title = "Day",
                    Labels = _reportBUS.EachDayConverter(startDate, endDate)
                });
            Title.Text = "Showing sales by day";
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _pageNavigation.NavigationService.GoBack();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedPhone == null)
            {
                MessageBox.Show("Please choose phone");
                return;
            }
            var startDate = StartDate.SelectedDate;
            var endDate = EndDate.SelectedDate;
            if (startDate == null || endDate == null) return;

            displayDateMode(_selectedPhone, (DateTime)startDate, (DateTime)endDate);
        }

        private void YearCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_selectedPhone == null)
            {
                //MessageBox.Show("Please choose phone");
                return;
            }

            int i = YearCombobox.SelectedIndex;
            if (i == -1)
            {
                return;
            }
            else
            {
                if (i == 1)
                {
                    displayMonthMode(_selectedPhone, 2023);
                    _currentYear = 2023;
                }
                if (i == 2)
                {
                    displayMonthMode(_selectedPhone, 2024);
                    _currentYear = 2024;
                }
                if (i == 3)
                {
                    displayMonthMode(_selectedPhone, 2025);
                    _currentYear = 2025;
                }
                if (i == 4)
                {
                    displayMonthMode(_selectedPhone, 2026);
                    _currentYear = 2026;
                }
                if ((i == 5))
                {
                    MonthCombobox.SelectedIndex = 0;
                    
                    StartDate.SelectedDate = null;
                    EndDate.SelectedDate = null;
                    MonthCombobox.IsEnabled = false;
                    displayYearMode(_selectedPhone);
                }
            }
        }

        private void MonthCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_selectedPhone == null)
            {
                //MessageBox.Show("Please choose phone");
                return;
            }
            
            int i = MonthCombobox.SelectedIndex;
            if (i == -1 || i == 0)
            {
                return;
            }
            else
            {
                displayWeekMode(_selectedPhone, i, _currentYear);
            }
        }

        private void PhoneCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedIndex = PhoneCombobox.SelectedIndex;
            _selectedPhone = PhoneCombobox.Items[_selectedIndex] as Phone;
            MonthCombobox.SelectedIndex = 0;
            YearCombobox.SelectedIndex = 0;
            StartDate.SelectedDate = null;
            EndDate.SelectedDate = null;
            MonthCombobox.IsEnabled = false;

            if (_selectedPhone != null)
            {
                displayYearMode(_selectedPhone);
            }
        }
    }
}
