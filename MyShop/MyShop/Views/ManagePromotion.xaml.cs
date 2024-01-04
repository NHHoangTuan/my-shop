using MyShop.BUS;
using MyShop.DTO;
using MyShop.ViewModel;
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
    /// Interaction logic for ManagePromotion.xaml
    /// </summary>
    public partial class ManagePromotion : Page
    {

        PromotionBUS _promoBUS;
        PromotionViewModel _promoVM;

        public ManagePromotion()
        {
            InitializeComponent();
            _promoVM = new PromotionViewModel();
            _promoBUS = new PromotionBUS();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _promoVM.promotions = _promoBUS.getAllPromotion();
            promotionListView.ItemsSource = _promoVM.promotions;
        }

        private void addPromotion_Click(object sender, RoutedEventArgs e)
        {
            if (PromotionCodeTermTextBox.Text == "")
            {
                MessageBox.Show("Please enter Code", "Notice", MessageBoxButton.OK);
                return;
            }
            if (PromotionValueTermTextBox.Text == "")
            {
                MessageBox.Show("Please enter discount value percent", "Notice", MessageBoxButton.OK);
                return;
            }

            Promotion newPromo = new Promotion()
            {
                promotionCode = PromotionCodeTermTextBox.Text.ToString(),
                discountPercent = int.Parse(PromotionValueTermTextBox.Text)
            };

            int promoID = _promoBUS.savePromotion(newPromo);
            newPromo.promotionID = promoID;

            _promoVM.promotions.Add(newPromo);

            PromotionCodeTermTextBox.Text = "";
            PromotionValueTermTextBox.Text = "";

        }

        private void RemovePromotion_Click(object sender, RoutedEventArgs e)
        {
            var p = (Promotion)promotionListView.SelectedItem;
            if (p != null)
            {
                var result = MessageBox.Show($"You really want to remove category?",
               "Confirm remove", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _promoBUS.deletePromotion(p);
                    _promoVM.promotions.Remove(p);

                }
            }

            else
            {
                MessageBox.Show("Please choose an item to delete");
            }
        }
    }
}
