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

namespace MyShop.Views
{
    /// <summary>
    /// Interaction logic for ManageOrders.xaml
    /// </summary>
    public partial class ManageOrders : Page
    {
        public ManageOrders()
        {
            InitializeComponent();
        }

        class OrderResources
        {
            public string FirstIcon { get; set; }
            public string LastIcon { get; set; }
            public string NextIcon { get; set; }
            public string PrevIcon { get; set; }
        }


        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            var addOrder = new AddOrder();
            addOrder.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            addOrder.ShowDialog();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new OrderResources()
            {
                FirstIcon = "Assets/Images/Design/first_icon.png",
                LastIcon = "Assets/Images/Design/last_icon.png",
                NextIcon = "Assets/Images/Design/next_icon.png",
                PrevIcon = "Assets/Images/Design/prev_icon.png"
            };
        }

        private void FirstButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LastButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
