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
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {

        class Data
        {
            public int totalPhone {  get; set; }
            public int totalOrderInMonth { get; set; }
            public int totalOrderInWeek { get; set; }
            public decimal totalRevenue { get; set; }

            public Data (int totalPhone, int totalOrderInMonth, int totalOrderInWeek, decimal totalRevenue)
            {
                this.totalPhone = totalPhone;
                this.totalOrderInMonth = totalOrderInMonth;
                this.totalOrderInWeek = totalOrderInWeek;
                this.totalRevenue = totalRevenue;
            }
        }

        DashboardBUS _dashboardBUS;
        ReportBUS _reportBUS;
        

        public Dashboard()
        {
            InitializeComponent();
            _dashboardBUS = new DashboardBUS();
            _reportBUS = new ReportBUS();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var totalPhone = _dashboardBUS.getTotalPhone();
            var totalOrderInMonth = _dashboardBUS.getTotalOrderInMonth(DateTime.Now.Year, DateTime.Now.Month);
            var totalOrderInWeek = _dashboardBUS.getTotalOrderInWeek(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var totalRevenue = _dashboardBUS.getTotalRevenue();
            Data data = new Data(totalPhone, totalOrderInMonth, totalOrderInWeek, totalRevenue);
            this.DataContext = data;

            List<Phone> list = _dashboardBUS.top5OutOfStock();
            
            top5OutOfStockListView.ItemsSource = list;
        }


      

        private void CartesianChart_Loaded(object sender, RoutedEventArgs e)
        {
            var quantityByYear = _reportBUS.groupPriceTotalByMonth(DateTime.Now.Year);

            var valuesLineChart = new ChartValues<double>();

            foreach (var item in quantityByYear)
            {
                valuesLineChart.Add((double)item);
            }



            chart.Series = new SeriesCollection();
            //chart.AxisX = new AxesCollection();

            chart.Series.Add(new LineSeries()
            {
                Stroke = Brushes.Chocolate,
                Title = "Revenue: ",
                Values = valuesLineChart
            });

           /* chart.AxisX.Add(
    new Axis()
    {
        Foreground = Brushes.Black,
        Title = "Month",
        Labels = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Des" },
        

    }); ;*/
            

        }

        
    }
}
