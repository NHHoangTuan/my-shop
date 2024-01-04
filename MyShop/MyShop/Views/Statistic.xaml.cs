using LiveCharts.Wpf;
using LiveCharts;
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
using MyShop.BUS;

namespace MyShop.Views
{
    /// <summary>
    /// Interaction logic for Statistic.xaml
    /// </summary>
    public partial class Statistic : Page
    {

        private ReportBUS _reportBUS;
        private CartesianChart _chart;
        private int startYear = 2023;
        private int endYear = 2026;
        private int _currentYear;
        private Frame _pageNavigation;

        public Statistic(Frame pageNavigation)
        {
            InitializeComponent();
            _pageNavigation = pageNavigation;
            _reportBUS = new ReportBUS();
            _chart = chart;

            _chart.AxisY.Add(new Axis
            {
                Foreground = Brushes.Black,
                Title = "Revenue / Profit",
                MinValue = 0
            });
            Title.Text = "Showing revenue and profit by year";
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var pricesByYear = _reportBUS.groupPriceTotalByYear(startYear, endYear);
            var profitsByYear = _reportBUS.groupProfitTotalByYear(startYear, endYear);

            //var valuesLineChart = new ChartValues<double>();
            var valuesCol1Chart = new ChartValues<double>();
            var valuesColChart = new ChartValues<double>();

            foreach (var item in pricesByYear)
            {
                valuesColChart.Add((double)item);
            }

            foreach (var item in profitsByYear)
            {
                //valuesLineChart.Add((double)item);
                valuesCol1Chart.Add((double)item);
            }

            _chart.Series = new SeriesCollection();
            _chart.AxisX = new AxesCollection();

            _chart.Series.Add(new ColumnSeries()
            {
                Fill = Brushes.DeepSkyBlue,
                Title = "Profit by year",
                //Values = valuesLineChart
                Values = valuesCol1Chart
            });

            _chart.Series.Add(new ColumnSeries()
            {
                Fill = Brushes.Chocolate,
                Title = "Revenue by year",
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
            Title.Text = "Showing revenue and profit by year";
        }



        private void displayMonthMode(int year)
        {
            
            var pricesByMonth = _reportBUS.groupPriceTotalByMonth(year);
            var profitsByMonth = _reportBUS.groupProfitTotalByMonth(year);
            
            var valuesCol1Chart = new ChartValues<double>();
            var valuesColChart = new ChartValues<double>();

            foreach (var item in pricesByMonth)
            {
                valuesColChart.Add((double)item);
            }

            foreach (var item in profitsByMonth)
            {
                valuesCol1Chart.Add((double)item);
            }

            _chart.Series = new SeriesCollection();
            _chart.AxisX = new AxesCollection();

            _chart.Series.Add(new ColumnSeries()
            {
                Fill = Brushes.DeepSkyBlue,
                Title = "Profit by month",
                Values = valuesCol1Chart
            });

            _chart.Series.Add(new ColumnSeries()
            {
                Fill = Brushes.Chocolate,
                Title = "Revenue by month",
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
            Title.Text = "Showing revenue and profit by month";
            MonthCombobox.IsEnabled = true;
            MonthCombobox.SelectedIndex = 0;
        }


        private void displayWeekMode(int month, int year)
        {
            
            var pricesByWeek =  _reportBUS.groupPriceTotalByWeek(month, year);
            var profitsByWeek =  _reportBUS.groupProfitTotalByWeek(month, year);
            
            var valuesCol1Chart = new ChartValues<double>();
            var valuesColChart = new ChartValues<double>();

            foreach (var item in pricesByWeek)
            {
                valuesColChart.Add((double)item);
            }

            foreach (var item in profitsByWeek)
            {
                valuesCol1Chart.Add((double)item);
            }

            _chart.Series = new SeriesCollection();
            _chart.AxisX = new AxesCollection();

            _chart.Series.Add(new ColumnSeries()
            {
                Fill = Brushes.DeepSkyBlue,
                Title = "Profit by week",
                Values = valuesCol1Chart
            });

            _chart.Series.Add(new ColumnSeries()
            {
                Fill = Brushes.Chocolate,
                Title = "Revenue by week",
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
            Title.Text = "Showing revenue and profit by week";
        }


        private void displayDateMode(DateTime startDate, DateTime endDate)
        {
            
            var pricesByDate =  _reportBUS.groupPriceTotalByDate(startDate, endDate);
            var profitsByDate =  _reportBUS.groupProfitTotalByDate(startDate, endDate);
            
            var valuesCol1Chart = new ChartValues<double>();
            var valuesColChart = new ChartValues<double>();

            foreach (var item in pricesByDate)
            {
                valuesColChart.Add((double)item);
            }

            foreach (var item in profitsByDate)
            {
                valuesCol1Chart.Add((double)item);
            }

            _chart.Series = new SeriesCollection();
            _chart.AxisX = new AxesCollection();

            _chart.Series.Add(new ColumnSeries()
            {
                Fill = Brushes.DeepSkyBlue,
                Title = "Profit by day",
                Values = valuesCol1Chart
            });

            _chart.Series.Add(new ColumnSeries()
            {
                Fill = Brushes.Chocolate,
                Title = "Revenue by day",
                Values = valuesColChart
            });

            chart.AxisX.Add(
                new Axis()
                {
                    Foreground = Brushes.Black,
                    Title = "Day",
                    Labels = _reportBUS.EachDayConverter(startDate, endDate)
                });
            Title.Text = "Showing revenue and profit by day";
        }


        private void MonthCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = MonthCombobox.SelectedIndex;
            if (i == -1 || i == 0)
            {
                return;
            }
            else
            {
                displayWeekMode(i, _currentYear);
            }
        }

        private void YearCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = YearCombobox.SelectedIndex;
            if (i == -1)
            {
                return;
            }
            else
            {
                if (i == 1)
                {
                    displayMonthMode(2023);
                    _currentYear = 2023;
                }
                if (i == 2)
                {
                    displayMonthMode(2024);
                    _currentYear = 2024;
                }
                if (i == 3)
                {
                    displayMonthMode(2025);
                    _currentYear = 2025;
                }
                if (i == 4)
                {
                    displayMonthMode(2026);
                    _currentYear = 2026;
                }
                if ((i == 5))
                {
                    Page_Loaded(sender, e);
                }
            }
        }

        private void productStatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            _pageNavigation.NavigationService.Navigate(new ProductStatistic(_pageNavigation));
        }

        private void sellingProductsButton_Click(object sender, RoutedEventArgs e)
        {
            _pageNavigation.NavigationService.Navigate(new AdvancedStatistic(_pageNavigation));
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            var startDate = StartDate.SelectedDate;
            var endDate = EndDate.SelectedDate;
            if (startDate == null || endDate == null) return;

            displayDateMode((DateTime)startDate, (DateTime)endDate);
        }

        private void PhoneCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
